﻿using StoreDashboard.Blazor.Application.Common.Extensions;
using StoreDashboard.Blazor.Application.Common.Interfaces;
using StoreDashboard.Blazor.Application.Common.Interfaces.Caching;
using StoreDashboard.Blazor.Application.Common.Models;
using StoreDashboard.Blazor.Application.Features.AuditTrails.Caching;
using StoreDashboard.Blazor.Application.Features.AuditTrails.DTOs;
using StoreDashboard.Blazor.Application.Features.AuditTrails.Specifications;
using StoreDashboard.Blazor.Domain.Entities;

namespace StoreDashboard.Blazor.Application.Features.AuditTrails.Queries.PaginationQuery;

public class AuditTrailsWithPaginationQuery : AuditTrailAdvancedFilter, ICacheableRequest<PaginatedData<AuditTrailDto>>
{
    public AuditTrailAdvancedSpecification Specification => new(this);
    public string CacheKey => AuditTrailsCacheKey.GetPaginationCacheKey($"{this}");
    public IEnumerable<string>? Tags => AuditTrailsCacheKey.Tags;

    public override string ToString()
    {
            return
                $"Listview:{ListView}-{CurrentUser?.UserId},AuditType:{AuditType},Search:{Keyword},Sort:{SortDirection},OrderBy:{OrderBy},{PageNumber},{PageSize}";
        }
}

public class AuditTrailsQueryHandler : IRequestHandler<AuditTrailsWithPaginationQuery, PaginatedData<AuditTrailDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public AuditTrailsQueryHandler(
        IApplicationDbContext context,
        IMapper mapper
    )
    {
            _context = context;
            _mapper = mapper;
        }

    public async Task<PaginatedData<AuditTrailDto>> Handle(AuditTrailsWithPaginationQuery request,
        CancellationToken cancellationToken)
    {
            var data = await _context.AuditTrails.OrderBy($"{request.OrderBy} {request.SortDirection}")
                .ProjectToPaginatedDataAsync<AuditTrail, AuditTrailDto>(request.Specification, request.PageNumber,
                    request.PageSize, _mapper.ConfigurationProvider, cancellationToken);

            return data;
        }
}