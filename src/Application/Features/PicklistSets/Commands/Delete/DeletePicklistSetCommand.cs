using StoreDashboard.Blazor.Application.Common.Interfaces;
using StoreDashboard.Blazor.Application.Common.Interfaces.Caching;
using StoreDashboard.Blazor.Application.Common.Models;
using StoreDashboard.Blazor.Application.Features.PicklistSets.Caching;
using StoreDashboard.Blazor.Domain.Common.Events;
using StoreDashboard.Blazor.Domain.Entities;

namespace StoreDashboard.Blazor.Application.Features.PicklistSets.Commands.Delete;

public class DeletePicklistSetCommand : ICacheInvalidatorRequest<Result>
{
    public DeletePicklistSetCommand(int[] id)
    {
            Id = id;
        }

    public int[] Id { get; }
    public string CacheKey => PicklistSetCacheKey.GetAllCacheKey;
    public IEnumerable<string>? Tags => PicklistSetCacheKey.Tags;
}

public class DeletePicklistSetCommandHandler : IRequestHandler<DeletePicklistSetCommand, Result>

{
    private readonly IApplicationDbContext _context;

    public DeletePicklistSetCommandHandler(
        IApplicationDbContext context
    )
    {
            _context = context;
        }

    public async Task<Result> Handle(DeletePicklistSetCommand request, CancellationToken cancellationToken)
    {
            var items = await _context.PicklistSets.Where(x => request.Id.Contains(x.Id)).ToListAsync(cancellationToken);
            foreach (var item in items)
            {
                var changeEvent = new UpdatedEvent<PicklistSet>(item);
                item.AddDomainEvent(changeEvent);
                _context.PicklistSets.Remove(item);
            }

            await _context.SaveChangesAsync(cancellationToken);
            return await Result.SuccessAsync();
        }
}