﻿using StoreDashboard.Blazor.Application.Common.Extensions;
using StoreDashboard.Blazor.Application.Common.Interfaces;
using StoreDashboard.Blazor.Application.Common.Models;
using StoreDashboard.Blazor.Application.Features.Products.DTOs;
using StoreDashboard.Blazor.Application.Features.Products.Specifications;
using StoreDashboard.Blazor.Domain.Common.Enums;

namespace StoreDashboard.Blazor.Application.Features.Products.Queries.Export;

public class ExportProductsQuery : ProductAdvancedFilter, IRequest<Result<byte[]>>
{
    public ExportType ExportType { get; set; }
    public ProductAdvancedSpecification Specification => new(this);
}

public class ExportProductsQueryHandler :
    IRequestHandler<ExportProductsQuery, Result<byte[]>>
{
    private readonly IApplicationDbContext _context;
    private readonly IExcelService _excelService;
    private readonly IMapper _mapper;
    private readonly IStringLocalizer<ExportProductsQueryHandler> _localizer;
    private readonly IPDFService _pdfService;

    public ExportProductsQueryHandler(
        IApplicationDbContext context,
        IExcelService excelService,
        IMapper mapper,
        IPDFService pdfService,
        IStringLocalizer<ExportProductsQueryHandler> localizer
    )
    {
        _context = context;
        _excelService = excelService;
        _mapper = mapper;
        _pdfService = pdfService;
        _localizer = localizer;
    }
#nullable disable warnings
    public async Task<Result<byte[]>> Handle(ExportProductsQuery request, CancellationToken cancellationToken)
    {
        var data = await _context.Products.ApplySpecification(request.Specification)
            .AsNoTracking()
            .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);


        byte[] result;
        Dictionary<string, Func<ProductDto, object?>> mappers;
        switch (request.ExportType)
        {
            case ExportType.PDF:
                mappers = new Dictionary<string, Func<ProductDto, object?>>
                {
                    { _localizer["Brand Name"], item => item.Brand },
                    { _localizer["Product Name"], item => item.Name },
                    { _localizer["Description"], item => item.Description },
                    { _localizer["Price of unit"], item => item.Price },
                    { _localizer["Unit"], item => item.Unit }
                    //{ _localizer["Pictures"], item => string.Join(",",item.Pictures??new string[]{ }) },
                };
                result = await _pdfService.ExportAsync(data, mappers, _localizer["Products"], true);
                break;
            default:
                mappers = new Dictionary<string, Func<ProductDto, object?>>
                {
                    { _localizer["Brand Name"], item => item.Brand },
                    { _localizer["Product Name"], item => item.Name },
                    { _localizer["Description"], item => item.Description },
                    { _localizer["Price of unit"], item => item.Price },
                    { _localizer["Unit"], item => item.Unit },
                    { _localizer["Pictures"], item => JsonSerializer.Serialize(item.Pictures) }
                };
                result = await _excelService.ExportAsync(data, mappers, _localizer["Products"]);
                break;
        }

        return await Result<byte[]>.SuccessAsync(result);
    }
}