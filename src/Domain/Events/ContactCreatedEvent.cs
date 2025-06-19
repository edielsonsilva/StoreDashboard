using StoreDashboard.Blazor.Domain.Common;
using StoreDashboard.Blazor.Domain.Entities;

namespace StoreDashboard.Blazor.Domain.Events;

    public class ContactCreatedEvent : DomainEvent
    {
        public ContactCreatedEvent(Contact item)
        {
            Item = item;
        }

        public Contact Item { get; }
    }

