using Hangfire;
using Microsoft.Extensions.DependencyInjection;
using StoreDashboard.Blazor.Application.Common.Interfaces;
using StoreDashboard.Blazor.Domain.Common.Events;
using StoreDashboard.Blazor.Domain.Entities;

namespace StoreDashboard.Blazor.Application.Features.Documents.EventHandlers;

public class DocumentCreatedEventHandler : INotificationHandler<CreatedEvent<Document>>
{
    private readonly ILogger<DocumentCreatedEventHandler> _logger;
    private readonly IServiceScopeFactory _scopeFactory;


    public DocumentCreatedEventHandler(
        IServiceScopeFactory scopeFactory,
        ILogger<DocumentCreatedEventHandler> logger
    )
    {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

    public Task Handle(CreatedEvent<Document> notification, CancellationToken cancellationToken)
    {
            _logger.LogInformation(
                "Document upload successful. Beginning OCR recognition process for Document Id: {DocumentId}",
                notification.Entity.Id);

            using (var scope = _scopeFactory.CreateScope())
            {
                var ocrJob = scope.ServiceProvider.GetRequiredService<IDocumentOcrJob>();
                BackgroundJob.Enqueue(() => ocrJob.Do(notification.Entity.Id));
            }

            return Task.CompletedTask;
        }
}