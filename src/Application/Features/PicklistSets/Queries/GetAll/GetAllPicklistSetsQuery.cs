using StoreDashboard.Blazor.Application.Common.Interfaces;
using StoreDashboard.Blazor.Application.Common.Interfaces.Caching;
using StoreDashboard.Blazor.Application.Features.PicklistSets.Caching;
using StoreDashboard.Blazor.Application.Features.PicklistSets.DTOs;

namespace StoreDashboard.Blazor.Application.Features.PicklistSets.Queries.GetAll;

public class GetAllPicklistSetsQuery : ICacheableRequest<IEnumerable<PicklistSetDto>>
{
    public string CacheKey => PicklistSetCacheKey.GetAllCacheKey;
    public IEnumerable<string>? Tags => PicklistSetCacheKey.Tags;
}

public class GetAllPicklistSetsQueryHandler : IRequestHandler<GetAllPicklistSetsQuery, IEnumerable<PicklistSetDto>>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _context;

    public GetAllPicklistSetsQueryHandler(
        IMapper mapper,
        IApplicationDbContext context)
    {
            _mapper = mapper;
            _context = context;
        }

    public async Task<IEnumerable<PicklistSetDto>> Handle(GetAllPicklistSetsQuery request,
        CancellationToken cancellationToken)
    {
            var data = await _context.PicklistSets.OrderBy(x => x.Name).ThenBy(x => x.Value)
                .ProjectTo<PicklistSetDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            return data;
        }
}