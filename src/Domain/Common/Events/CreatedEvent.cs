using StoreDashboard.Blazor.Domain.Common.Entities;

namespace StoreDashboard.Blazor.Domain.Common.Events;

public class CreatedEvent<T> : DomainEvent where T : IEntity
{
    public CreatedEvent(T entity)
    {
            Entity = entity;
        }

    public T Entity { get; }
}