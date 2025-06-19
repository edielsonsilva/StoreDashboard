#nullable enable
using StoreDashboard.Blazor.Application.Features.Contacts.DTOs;
using StoreDashboard.Blazor.Application.Features.Contacts.Caching;
using StoreDashboard.Blazor.Application.Common.Interfaces;
using StoreDashboard.Blazor.Application.Common.Interfaces.Caching;

namespace StoreDashboard.Blazor.Application.Features.Contacts.Queries.GetAll;

public class GetAllContactsQuery : ICacheableRequest<IEnumerable<ContactDto>>
{
   public string CacheKey => ContactCacheKey.GetAllCacheKey;
   public IEnumerable<string>? Tags => ContactCacheKey.Tags;
}

public class GetAllContactsQueryHandler :
     IRequestHandler<GetAllContactsQuery, IEnumerable<ContactDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    public GetAllContactsQueryHandler(
        IMapper mapper,
        IApplicationDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<IEnumerable<ContactDto>> Handle(GetAllContactsQuery request, CancellationToken cancellationToken)
    {
        var data = await _context.Contacts.ProjectTo<ContactDto>(_mapper.ConfigurationProvider)
                                                .AsNoTracking()
                                                .ToListAsync(cancellationToken);
        return data;
    }
}


