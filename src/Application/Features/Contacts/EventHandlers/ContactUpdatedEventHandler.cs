namespace StoreDashboard.Blazor.Application.Features.Contacts.EventHandlers;

    public class ContactUpdatedEventHandler : INotificationHandler<ContactUpdatedEvent>
    {
        private readonly ILogger<ContactUpdatedEventHandler> _logger;

        public ContactUpdatedEventHandler(
            ILogger<ContactUpdatedEventHandler> logger
            )
        {
            _logger = logger;
        }
        public Task Handle(ContactUpdatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handled domain event '{EventType}' with notification: {@Notification} ", notification.GetType().Name, notification);
            return Task.CompletedTask;
        }
    }
