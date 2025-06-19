#nullable enable
using StoreDashboard.Blazor.Application.Features.Contacts.DTOs;
using StoreDashboard.Blazor.Application.Features.Contacts.Caching;
using StoreDashboard.Blazor.Application.Features.Contacts.Specifications;
using StoreDashboard.Blazor.Application.Common.ExceptionHandlers;
using StoreDashboard.Blazor.Application.Common.Extensions;
using StoreDashboard.Blazor.Application.Common.Interfaces;
using StoreDashboard.Blazor.Application.Common.Interfaces.Caching;
using StoreDashboard.Blazor.Application.Common.Models;

namespace StoreDashboard.Blazor.Application.Features.Contacts.Queries.GetById;

public class GetContactByIdQuery : ICacheableRequest<Result<ContactDto>>
{
   public required int Id { get; set; }
   public string CacheKey => ContactCacheKey.GetByIdCacheKey($"{Id}");
   public IEnumerable<string>? Tags => ContactCacheKey.Tags;
}

public class GetContactByIdQueryHandler :
     IRequestHandler<GetContactByIdQuery, Result<ContactDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    public GetContactByIdQueryHandler(
        IMapper mapper,
        IApplicationDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<Result<ContactDto>> Handle(GetContactByIdQuery request, CancellationToken cancellationToken)
    {
        var data = await _context.Contacts.ApplySpecification(new ContactByIdSpecification(request.Id))
                                                .ProjectTo<ContactDto>(_mapper.ConfigurationProvider)
                                                .FirstAsync(cancellationToken) ?? throw new NotFoundException($"Contact with id: [{request.Id}] not found.");
        return await Result<ContactDto>.SuccessAsync(data);
    }
}
