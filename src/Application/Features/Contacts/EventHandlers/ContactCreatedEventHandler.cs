namespace StoreDashboard.Blazor.Application.Features.Contacts.EventHandlers;

public class ContactCreatedEventHandler : INotificationHandler<ContactCreatedEvent>
{
        private readonly ILogger<ContactCreatedEventHandler> _logger;

        public ContactCreatedEventHandler(
            ILogger<ContactCreatedEventHandler> logger
            )
        {
            _logger = logger;
        }
        public Task Handle(ContactCreatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handled domain event '{EventType}' with notification: {@Notification} ", notification.GetType().Name, notification);
            return Task.CompletedTask;
        }
}
