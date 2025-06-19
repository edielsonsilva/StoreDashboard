﻿using System.Diagnostics;
using System.Text.Json.Serialization;
using StoreDashboard.Blazor.Application.Common.Interfaces;
using StoreDashboard.Blazor.Application.Features.Documents.Caching;
using StoreDashboard.Blazor.Domain.Common.Enums;

namespace StoreDashboard.Blazor.Infrastructure.Services.PaddleOCR;

public class DocumentOcrJob : IDocumentOcrJob
{
    private readonly IApplicationDbContext _context;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<DocumentOcrJob> _logger;
    private readonly IApplicationHubWrapper _notificationService;
    private readonly Stopwatch _timer;

    public DocumentOcrJob(
        IApplicationHubWrapper appNotificationService,
        IApplicationDbContext context,
        IHttpClientFactory httpClientFactory,
        ILogger<DocumentOcrJob> logger)
    {
            _notificationService = appNotificationService;
            _context = context;
            _httpClientFactory = httpClientFactory;
            _logger = logger;
            _timer = new Stopwatch();
        }

    public void Do(int id)
    {
            Recognition(id, CancellationToken.None).Wait();
        }

    public async Task Recognition(int id, CancellationToken cancellationToken)
    {
            try
            {
                using (var client = _httpClientFactory.CreateClient("ocr"))
                {
                    _timer.Start();

                    var doc = await _context.Documents.FindAsync(id);
                    if (doc == null)
                    {
                        _logger.LogWarning("Document with Id {Id} not found.", id);
                        return;
                    }

                    await _notificationService.JobStarted(id, doc.Title!);
                    CancelCacheToken();

                    if (string.IsNullOrEmpty(doc.URL))
                    {
                        _logger.LogWarning("Document URL is null or empty for Id {Id}.", id);
                        return;
                    }

                    var response = await client.GetAsync($"?imageUrl={doc.URL}");

                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        if (result.Length > 4000)
                        {
                            result = result.Substring(0, 4000);
                        }

                        doc.Status = JobStatus.Done;
                        doc.Description = "Recognition result: success";
                        doc.Content = result;

                        await _context.SaveChangesAsync(cancellationToken);
                        await _notificationService.JobCompleted(id, doc.Title!);
                        CancelCacheToken();

                        _timer.Stop();
                        _logger.LogInformation(
                            "Image recognition completed successfully {@Document}. Id: {Id}, Elapsed Time: {ElapsedMilliseconds}ms", doc,
                            id, _timer.ElapsedMilliseconds);
                    }
                    else
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        doc.Status = JobStatus.Pending;
                        doc.Content = result;

                        await _context.SaveChangesAsync(cancellationToken);
                        await _notificationService.JobCompleted(id, $"Error: {result}");
                        CancelCacheToken();

                        _logger.LogError("Image recognition failed for Id: {Id}, Status Code: {StatusCode}, Message: {Message}",
                            id, response.StatusCode, result);
                    }
                }
            }
            catch (Exception ex)
            {
                await _notificationService.JobCompleted(id, $"Error: {ex.Message}");
                _logger.LogError(ex, "Image recognition error for Id: {Id}, Message: {Message}", id, ex.Message);
            }
            finally
            {
                if (_timer.IsRunning)
                {
                    _timer.Stop();
                }
            }
        }

    private void CancelCacheToken()
    {
            DocumentCacheKey.Refresh();
        }
}
#pragma warning disable CS8981
internal class OcrResult
{
    [JsonPropertyName("resultcode")] public string? ResultCode { get; set; }

    [JsonPropertyName("message")] public string? Message { get; set; }

    [JsonPropertyName("data")] public List<List<List<dynamic>>>? Data { get; set; }
}
#pragma warning restore CS8981