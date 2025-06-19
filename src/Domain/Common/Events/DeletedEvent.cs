using StoreDashboard.Blazor.Domain.Common.Entities;

namespace StoreDashboard.Blazor.Domain.Common.Events;

public class DeletedEvent<T> : DomainEvent where T : IEntity
{
    public DeletedEvent(T entity)
    {
            Entity = entity;
        }

    public T Entity { get; }
}