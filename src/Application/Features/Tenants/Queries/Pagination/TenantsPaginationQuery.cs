﻿using StoreDashboard.Blazor.Application.Common.Extensions;
using StoreDashboard.Blazor.Application.Common.Interfaces;
using StoreDashboard.Blazor.Application.Common.Interfaces.Caching;
using StoreDashboard.Blazor.Application.Common.Models;
using StoreDashboard.Blazor.Application.Features.Tenants.Caching;
using StoreDashboard.Blazor.Application.Features.Tenants.DTOs;
using StoreDashboard.Blazor.Domain.Entities;

namespace StoreDashboard.Blazor.Application.Features.Tenants.Queries.Pagination;

public class TenantsWithPaginationQuery : PaginationFilter, ICacheableRequest<PaginatedData<TenantDto>>
{
    public TenantsPaginationSpecification Specification => new(this);
    public string CacheKey => TenantCacheKey.GetPaginationCacheKey($"{this}");
    public IEnumerable<string>? Tags => TenantCacheKey.Tags;

    public override string ToString()
    {
        return $"Search:{Keyword},OrderBy:{OrderBy} {SortDirection},{PageNumber},{PageSize}";
    }
}

public class TenantsWithPaginationQueryHandler :
    IRequestHandler<TenantsWithPaginationQuery, PaginatedData<TenantDto>>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _context;

    public TenantsWithPaginationQueryHandler(
        IMapper mapper,
        IApplicationDbContext context
    )
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<PaginatedData<TenantDto>> Handle(TenantsWithPaginationQuery request,
        CancellationToken cancellationToken)
    {
        var data = await _context.Tenants.OrderBy($"{request.OrderBy} {request.SortDirection}")
            .ProjectToPaginatedDataAsync<Tenant, TenantDto>(request.Specification, request.PageNumber, request.PageSize,
                _mapper.ConfigurationProvider, cancellationToken);
        return data;
    }
}
#nullable disable warnings
public class TenantsPaginationSpecification : Specification<Tenant>
{
    public TenantsPaginationSpecification(TenantsWithPaginationQuery query)
    {
        Query.Where(q => q.Name != null)
            .Where(q => q.Name.Contains(query.Keyword) || q.Description.Contains(query.Keyword),
                !string.IsNullOrEmpty(query.Keyword));
    }
}