using StoreDashboard.Blazor.Application.Common.Interfaces;
using StoreDashboard.Blazor.Application.Common.Interfaces.Caching;
using StoreDashboard.Blazor.Application.Features.Tenants.Caching;
using StoreDashboard.Blazor.Application.Features.Tenants.DTOs;

namespace StoreDashboard.Blazor.Application.Features.Tenants.Queries.GetAll;

public class GetAllTenantsQuery : ICacheableRequest<IEnumerable<TenantDto>>
{
    public string CacheKey => TenantCacheKey.GetAllCacheKey;
    public IEnumerable<string>? Tags => TenantCacheKey.Tags;
}

public class GetAllTenantsQueryHandler :
    IRequestHandler<GetAllTenantsQuery, IEnumerable<TenantDto>>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _context;
    public GetAllTenantsQueryHandler(
        IMapper mapper,
        IApplicationDbContext context
    )
    {
            _mapper = mapper;
            _context = context;
        }

    public async Task<IEnumerable<TenantDto>> Handle(GetAllTenantsQuery request, CancellationToken cancellationToken)
    {
            var data = await _context.Tenants
                .ProjectTo<TenantDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            return data;
        }
}