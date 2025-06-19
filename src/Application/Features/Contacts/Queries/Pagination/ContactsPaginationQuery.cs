#nullable enable
using StoreDashboard.Blazor.Application.Features.Contacts.DTOs;
using StoreDashboard.Blazor.Application.Features.Contacts.Caching;
using StoreDashboard.Blazor.Application.Features.Contacts.Specifications;
using StoreDashboard.Blazor.Application.Common.Extensions;
using StoreDashboard.Blazor.Application.Common.Interfaces;
using StoreDashboard.Blazor.Application.Common.Interfaces.Caching;
using StoreDashboard.Blazor.Application.Common.Models;
using StoreDashboard.Blazor.Domain.Entities;

namespace StoreDashboard.Blazor.Application.Features.Contacts.Queries.Pagination;

public class ContactsWithPaginationQuery : ContactAdvancedFilter, ICacheableRequest<PaginatedData<ContactDto>>
{
    public override string ToString()
    {
        return $"Listview:{ListView}:{CurrentUser?.UserId}, Search:{Keyword}, {OrderBy}, {SortDirection}, {PageNumber}, {PageSize}";
    }
    public string CacheKey => ContactCacheKey.GetPaginationCacheKey($"{this}");
    public IEnumerable<string>? Tags => ContactCacheKey.Tags;
    public ContactAdvancedSpecification Specification => new ContactAdvancedSpecification(this);
}
    
public class ContactsWithPaginationQueryHandler :
         IRequestHandler<ContactsWithPaginationQuery, PaginatedData<ContactDto>>
{
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public ContactsWithPaginationQueryHandler(
            IMapper mapper,
            IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<PaginatedData<ContactDto>> Handle(ContactsWithPaginationQuery request, CancellationToken cancellationToken)
        {
           var data = await _context.Contacts.OrderBy($"{request.OrderBy} {request.SortDirection}")
                                                   .ProjectToPaginatedDataAsync<Contact, ContactDto>(request.Specification,
                                                    request.PageNumber,
                                                    request.PageSize,
                                                    _mapper.ConfigurationProvider,
                                                    cancellationToken);
            return data;
        }
}