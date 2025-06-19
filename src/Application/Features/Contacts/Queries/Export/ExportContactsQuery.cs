#nullable enable
using StoreDashboard.Blazor.Application.Features.Contacts.DTOs;
using StoreDashboard.Blazor.Application.Features.Contacts.Caching;
using StoreDashboard.Blazor.Application.Features.Contacts.Specifications;
using StoreDashboard.Blazor.Application.Common.Extensions;
using StoreDashboard.Blazor.Application.Common.Interfaces;
using StoreDashboard.Blazor.Application.Common.Interfaces.Caching;
using StoreDashboard.Blazor.Application.Common.Models;

namespace StoreDashboard.Blazor.Application.Features.Contacts.Queries.Export;

public class ExportContactsQuery : ContactAdvancedFilter, ICacheableRequest<Result<byte[]>>
{
      public ContactAdvancedSpecification Specification => new ContactAdvancedSpecification(this);
      public IEnumerable<string>? Tags => ContactCacheKey.Tags;
    public override string ToString()
    {
        return $"Listview:{ListView}:{CurrentUser?.UserId}, Search:{Keyword}, {OrderBy}, {SortDirection}";
    }
    public string CacheKey => ContactCacheKey.GetExportCacheKey($"{this}");
}
    
public class ExportContactsQueryHandler :
         IRequestHandler<ExportContactsQuery, Result<byte[]>>
{
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;
        private readonly IExcelService _excelService;
        private readonly IStringLocalizer<ExportContactsQueryHandler> _localizer;
        private readonly ContactDto _dto = new();
        public ExportContactsQueryHandler(
            IMapper mapper,
            IApplicationDbContext context,
            IExcelService excelService,
            IStringLocalizer<ExportContactsQueryHandler> localizer
            )
        {
            _mapper = mapper;
            _context = context;
            _excelService = excelService;
            _localizer = localizer;
        }
        #nullable disable warnings
        public async Task<Result<byte[]>> Handle(ExportContactsQuery request, CancellationToken cancellationToken)
        {
            var data = await _context.Contacts.ApplySpecification(request.Specification)
                       .OrderBy($"{request.OrderBy} {request.SortDirection}")
                       .ProjectTo<ContactDto>(_mapper.ConfigurationProvider)
                       .AsNoTracking()
                       .ToListAsync(cancellationToken);
            var result = await _excelService.ExportAsync(data,
                new Dictionary<string, Func<ContactDto, object?>>()
                {
                    // TODO: Define the fields that should be exported, for example:
                                     {_localizer[_dto.GetMemberDescription(x=>x.Name)],item => item.Name}, 
                 {_localizer[_dto.GetMemberDescription(x=>x.Description)],item => item.Description}, 
                 {_localizer[_dto.GetMemberDescription(x=>x.Email)],item => item.Email}, 
                 {_localizer[_dto.GetMemberDescription(x=>x.PhoneNumber)],item => item.PhoneNumber}, 
                 {_localizer[_dto.GetMemberDescription(x=>x.Country)],item => item.Country}, 

                }
                , _localizer[_dto.GetClassDescription()]);
            return await Result<byte[]>.SuccessAsync(result);
        }
}
