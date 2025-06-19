using StoreDashboard.Blazor.Application.Common.Extensions;
using StoreDashboard.Blazor.Domain.Common.Enums;
using StoreDashboard.Blazor.Domain.Common.Events;
using StoreDashboard.Blazor.Domain.Entities;

namespace StoreDashboard.Blazor.Application.Features.Documents.EventHandlers;

public class DocumentDeletedEventHandler : INotificationHandler<DeletedEvent<Document>>
{
    private readonly ILogger<DocumentDeletedEventHandler> _logger;

    public DocumentDeletedEventHandler(ILogger<DocumentDeletedEventHandler> logger)
    {
            _logger = logger;
        }

    public Task Handle(DeletedEvent<Document> notification, CancellationToken cancellationToken)
    {
            if (string.IsNullOrEmpty(notification.Entity.URL))
            {
                _logger.LogWarning("The document URL is null or empty, skipping file deletion.");
                return Task.CompletedTask;
            }

            var folder = UploadType.Document.GetDescription();
            var folderName = Path.Combine("Files", folder);
            var deleteFilePath = Path.Combine(Directory.GetCurrentDirectory(), folderName, notification.Entity.URL);

            if (File.Exists(deleteFilePath))
            {
                try
                {
                    File.Delete(deleteFilePath);
                    _logger.LogInformation("File deleted successfully: {FilePath}", deleteFilePath);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to delete file: {FilePath}", deleteFilePath);
                }
            }
            else
            {
                _logger.LogWarning("File not found for deletion: {FilePath}", deleteFilePath);
            }

            return Task.CompletedTask;
        }
}