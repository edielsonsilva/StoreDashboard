using StoreDashboard.Blazor.Application.Common.Interfaces;
using StoreDashboard.Blazor.Domain.Common.Events;
using StoreDashboard.Blazor.Domain.Entities;

namespace StoreDashboard.Blazor.Application.Features.PicklistSets.EventHandlers;

public class PicklistSetChangedEventHandler : INotificationHandler<UpdatedEvent<PicklistSet>>
{
    private readonly ILogger<PicklistSetChangedEventHandler> _logger;
    private readonly IPicklistService _picklistService;

    public PicklistSetChangedEventHandler(
        IPicklistService picklistService,
        ILogger<PicklistSetChangedEventHandler> logger
    )
    {
            _picklistService = picklistService;
            _logger = logger;
        }

    public Task Handle(UpdatedEvent<PicklistSet> notification, CancellationToken cancellationToken)
    {
            _logger.LogInformation("Handled domain event '{EventType}' with notification: {@Notification} ",
                notification.GetType().Name,
                notification);
            _picklistService.Refresh();
            return Task.CompletedTask;
        }
}