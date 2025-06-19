using StoreDashboard.Blazor.Application.Common.Extensions;
using StoreDashboard.Blazor.Application.Common.Interfaces;
using StoreDashboard.Blazor.Application.Common.Interfaces.Caching;
using StoreDashboard.Blazor.Application.Common.Models;
using StoreDashboard.Blazor.Application.Features.Products.Caching;
using StoreDashboard.Blazor.Application.Features.Products.DTOs;
using StoreDashboard.Blazor.Application.Features.Products.Specifications;
using StoreDashboard.Blazor.Domain.Entities;

namespace StoreDashboard.Blazor.Application.Features.Products.Queries.Pagination;

public class ProductsWithPaginationQuery : ProductAdvancedFilter, ICacheableRequest<PaginatedData<ProductDto>>
{
    public ProductAdvancedSpecification Specification => new(this);
    public string CacheKey => ProductCacheKey.GetPaginationCacheKey($"{this}");

    public IEnumerable<string>? Tags => ProductCacheKey.Tags;

    // the currently logged in user
    public override string ToString()
    {
            return
                $"CurrentUser:{CurrentUser?.UserId},ListView:{ListView},Search:{Keyword},Name:{Name},Brand:{Brand},Unit:{Unit},MinPrice:{MinPrice},MaxPrice:{MaxPrice},SortDirection:{SortDirection},OrderBy:{OrderBy},{PageNumber},{PageSize}";
        }
}

public class ProductsWithPaginationQueryHandler :
    IRequestHandler<ProductsWithPaginationQuery, PaginatedData<ProductDto>>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _context;
    public ProductsWithPaginationQueryHandler(
        IMapper mapper,
        IApplicationDbContext context
    )
    {
            _mapper = mapper;
            _context = context;
        }

    public async Task<PaginatedData<ProductDto>> Handle(ProductsWithPaginationQuery request,
        CancellationToken cancellationToken)
    {
            var data = await _context.Products.OrderBy($"{request.OrderBy} {request.SortDirection}")
                .ProjectToPaginatedDataAsync<Product, ProductDto>(request.Specification, request.PageNumber,
                    request.PageSize, _mapper.ConfigurationProvider, cancellationToken);
            return data;
        }
}