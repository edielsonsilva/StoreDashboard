﻿using StoreDashboard.Blazor.Application.Common.Extensions;
using StoreDashboard.Blazor.Application.Common.Interfaces;
using StoreDashboard.Blazor.Application.Common.Interfaces.Caching;
using StoreDashboard.Blazor.Application.Common.Models;
using StoreDashboard.Blazor.Application.Features.PicklistSets.Caching;
using StoreDashboard.Blazor.Application.Features.PicklistSets.DTOs;
using StoreDashboard.Blazor.Application.Features.PicklistSets.Specifications;
using StoreDashboard.Blazor.Domain.Entities;

namespace StoreDashboard.Blazor.Application.Features.PicklistSets.Queries.PaginationQuery;

public class PicklistSetsWithPaginationQuery : PicklistSetAdvancedFilter, ICacheableRequest<PaginatedData<PicklistSetDto>>
{
    public PicklistSetAdvancedSpecification Specification => new(this);
    public string CacheKey => $"{nameof(PicklistSetsWithPaginationQuery)},{this}";
    public IEnumerable<string>? Tags => PicklistSetCacheKey.Tags;

    public override string ToString()
    {
            return $"ListView:{ListView}-{Picklist}-{CurrentUser?.UserId},Search:{Keyword},OrderBy:{OrderBy} {SortDirection},{PageNumber},{PageSize}";
        }
}

public class PicklistSetsQueryHandler : IRequestHandler<PicklistSetsWithPaginationQuery, PaginatedData<PicklistSetDto>>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _context;

    public PicklistSetsQueryHandler(
        IMapper mapper,
        IApplicationDbContext context)
    {
            _mapper = mapper;
            _context = context;
        }

    public async Task<PaginatedData<PicklistSetDto>> Handle(PicklistSetsWithPaginationQuery request,
        CancellationToken cancellationToken)
    {
            var data = await _context.PicklistSets.OrderBy($"{request.OrderBy} {request.SortDirection}")
                .ProjectToPaginatedDataAsync<PicklistSet, PicklistSetDto>(request.Specification, request.PageNumber,
                    request.PageSize, _mapper.ConfigurationProvider, cancellationToken);

            return data;
        }
}