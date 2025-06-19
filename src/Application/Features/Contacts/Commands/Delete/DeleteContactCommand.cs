#nullable enable
using StoreDashboard.Blazor.Application.Features.Contacts.Caching;
using StoreDashboard.Blazor.Application.Common.Interfaces;
using StoreDashboard.Blazor.Application.Common.Interfaces.Caching;
using StoreDashboard.Blazor.Application.Common.Models;


namespace StoreDashboard.Blazor.Application.Features.Contacts.Commands.Delete;

public class DeleteContactCommand:  ICacheInvalidatorRequest<Result>
{
  public int[] Id {  get; }
  public string CacheKey => ContactCacheKey.GetAllCacheKey;
  public IEnumerable<string>? Tags => ContactCacheKey.Tags;
  public DeleteContactCommand(int[] id)
  {
    Id = id;
  }
}

public class DeleteContactCommandHandler : 
             IRequestHandler<DeleteContactCommand, Result>

{
    private readonly IApplicationDbContext _context;
    public DeleteContactCommandHandler(
        IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Result> Handle(DeleteContactCommand request, CancellationToken cancellationToken)
    {
        var items = await _context.Contacts.Where(x=>request.Id.Contains(x.Id)).ToListAsync(cancellationToken);
        foreach (var item in items)
        {
		    // raise a delete domain event
			item.AddDomainEvent(new ContactDeletedEvent(item));
            _context.Contacts.Remove(item);
        }
        await _context.SaveChangesAsync(cancellationToken);
        return await Result.SuccessAsync();
    }

}

