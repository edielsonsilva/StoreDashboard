﻿using StoreDashboard.Blazor.Application.Common.Extensions;
using StoreDashboard.Blazor.Application.Common.Interfaces;
using StoreDashboard.Blazor.Application.Common.Interfaces.Caching;
using StoreDashboard.Blazor.Application.Common.Models;
using StoreDashboard.Blazor.Application.Features.Documents.Caching;
using StoreDashboard.Blazor.Application.Features.Documents.DTOs;
using StoreDashboard.Blazor.Application.Features.Documents.Specifications;
using StoreDashboard.Blazor.Domain.Entities;

namespace StoreDashboard.Blazor.Application.Features.Documents.Queries.PaginationQuery;

public class DocumentsWithPaginationQuery : AdvancedDocumentsFilter, ICacheableRequest<PaginatedData<DocumentDto>>
{
    public AdvancedDocumentsSpecification Specification => new(this);

    public string CacheKey => DocumentCacheKey.GetPaginationCacheKey($"{this}");
    public IEnumerable<string>? Tags => DocumentCacheKey.Tags;

    public override string ToString()
    {
            return
                $"CurrentUserId:{CurrentUser?.UserId},ListView:{ListView},Search:{Keyword},OrderBy:{OrderBy} {SortDirection},{PageNumber},{PageSize}";
        }
}

public class DocumentsQueryHandler : IRequestHandler<DocumentsWithPaginationQuery, PaginatedData<DocumentDto>>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _context;

    public DocumentsQueryHandler(
        IMapper mapper,
        IApplicationDbContext context
    )
    {
            _mapper = mapper;
            _context = context;
        }

    public async Task<PaginatedData<DocumentDto>> Handle(DocumentsWithPaginationQuery request,
        CancellationToken cancellationToken)
    {
            var data = await _context.Documents.OrderBy($"{request.OrderBy} {request.SortDirection}")
                .ProjectToPaginatedDataAsync<Document, DocumentDto>(request.Specification, request.PageNumber, request.PageSize, _mapper.ConfigurationProvider, cancellationToken);

            return data;
        }
}