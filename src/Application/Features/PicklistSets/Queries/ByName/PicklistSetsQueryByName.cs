using StoreDashboard.Blazor.Application.Common.Interfaces;
using StoreDashboard.Blazor.Application.Common.Interfaces.Caching;
using StoreDashboard.Blazor.Application.Features.PicklistSets.Caching;
using StoreDashboard.Blazor.Application.Features.PicklistSets.DTOs;
using StoreDashboard.Blazor.Domain.Entities;

namespace StoreDashboard.Blazor.Application.Features.PicklistSets.Queries.ByName;

public class PicklistSetsQueryByName : ICacheableRequest<IEnumerable<PicklistSetDto>>
{
    public PicklistSetsQueryByName(Picklist name)
    {
            Name = name;
        }
    public Picklist Name { get; set; }
    public string CacheKey => PicklistSetCacheKey.GetCacheKey(Name.ToString());
    public IEnumerable<string>? Tags => PicklistSetCacheKey.Tags;
}

public class PicklistSetsQueryByNameHandler : IRequestHandler<PicklistSetsQueryByName, IEnumerable<PicklistSetDto>>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _context;
    public PicklistSetsQueryByNameHandler(
        IMapper mapper,
        IApplicationDbContext context)
    {
            _mapper = mapper;
            _context = context;
        }

    public async Task<IEnumerable<PicklistSetDto>> Handle(PicklistSetsQueryByName request,
        CancellationToken cancellationToken)
    {
            var data = await _context.PicklistSets.Where(x => x.Name == request.Name)
                .OrderBy(x => x.Text)
                .ProjectTo<PicklistSetDto>(_mapper.ConfigurationProvider)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
            return data;
        }
}