namespace StoreDashboard.Blazor.Application.Features.Contacts.EventHandlers;

    public class ContactDeletedEventHandler : INotificationHandler<ContactDeletedEvent>
    {
        private readonly ILogger<ContactDeletedEventHandler> _logger;

        public ContactDeletedEventHandler(
            ILogger<ContactDeletedEventHandler> logger
            )
        {
            _logger = logger;
        }
        public Task Handle(ContactDeletedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handled domain event '{EventType}' with notification: {@Notification} ", notification.GetType().Name, notification);
            return Task.CompletedTask;
        }
    }
