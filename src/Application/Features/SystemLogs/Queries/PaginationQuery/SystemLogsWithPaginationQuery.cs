using StoreDashboard.Blazor.Application.Common.Extensions;
using StoreDashboard.Blazor.Application.Common.Interfaces;
using StoreDashboard.Blazor.Application.Common.Interfaces.Caching;
using StoreDashboard.Blazor.Application.Common.Models;
using StoreDashboard.Blazor.Application.Features.SystemLogs.Caching;
using StoreDashboard.Blazor.Application.Features.SystemLogs.DTOs;
using StoreDashboard.Blazor.Application.Features.SystemLogs.Specifications;
using StoreDashboard.Blazor.Domain.Entities;

namespace StoreDashboard.Blazor.Application.Features.SystemLogs.Queries.PaginationQuery;

public class SystemLogsWithPaginationQuery : SystemLogAdvancedFilter, ICacheableRequest<PaginatedData<SystemLogDto>>
{
    public SystemLogAdvancedSpecification Specification => new(this);

    public string CacheKey => SystemLogsCacheKey.GetPaginationCacheKey($"{this}");
    public IEnumerable<string>? Tags => SystemLogsCacheKey.Tags;

    public override string ToString()
    {
            return
                $"Listview:{ListView},{Level},Search:{Keyword},OrderBy:{OrderBy} {SortDirection},{PageNumber},{PageSize}";
        }
}

public class LogsQueryHandler : IRequestHandler<SystemLogsWithPaginationQuery, PaginatedData<SystemLogDto>>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _context;

    public LogsQueryHandler(
        IMapper mapper,
        IApplicationDbContext context
    )
    {
            _mapper = mapper;
            _context = context;
        }

    public async Task<PaginatedData<SystemLogDto>> Handle(SystemLogsWithPaginationQuery request,
        CancellationToken cancellationToken)
    {
            var data = await _context.SystemLogs.OrderBy($"{request.OrderBy} {request.SortDirection}")
                .ProjectToPaginatedDataAsync<SystemLog, SystemLogDto>(request.Specification, request.PageNumber, request.PageSize,
                    _mapper.ConfigurationProvider, cancellationToken);
            return data;
        }
}