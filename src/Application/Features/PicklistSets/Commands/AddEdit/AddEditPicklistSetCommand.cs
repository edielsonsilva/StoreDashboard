using StoreDashboard.Blazor.Application.Common.Interfaces;
using StoreDashboard.Blazor.Application.Common.Interfaces.Caching;
using StoreDashboard.Blazor.Application.Common.Models;
using StoreDashboard.Blazor.Application.Features.PicklistSets.Caching;
using StoreDashboard.Blazor.Application.Features.PicklistSets.DTOs;
using StoreDashboard.Blazor.Domain.Common.Enums;
using StoreDashboard.Blazor.Domain.Common.Events;
using StoreDashboard.Blazor.Domain.Entities;

namespace StoreDashboard.Blazor.Application.Features.PicklistSets.Commands.AddEdit;

public class AddEditPicklistSetCommand : ICacheInvalidatorRequest<Result<int>>
{
    [Description("Id")] public int Id { get; set; }
    [Description("Name")] public Picklist Name { get; set; }
    [Description("Value")] public string? Value { get; set; }
    [Description("Text")] public string? Text { get; set; }
    [Description("Description")] public string? Description { get; set; }
    public TrackingState TrackingState { get; set; } = TrackingState.Unchanged;
    public string CacheKey => PicklistSetCacheKey.GetAllCacheKey;
    public IEnumerable<string>? Tags => PicklistSetCacheKey.Tags;
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<PicklistSetDto, AddEditPicklistSetCommand>(MemberList.None);
            CreateMap<AddEditPicklistSetCommand, PicklistSet>(MemberList.None);
        }
    }
}

public class AddEditPicklistSetCommandHandler : IRequestHandler<AddEditPicklistSetCommand, Result<int>>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _context;

    public AddEditPicklistSetCommandHandler(
        IMapper mapper,
        IApplicationDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<Result<int>> Handle(AddEditPicklistSetCommand request, CancellationToken cancellationToken)
    {
        if (request.Id > 0)
        {
            var item = await _context.PicklistSets.FindAsync(request.Id, cancellationToken);
            if (item == null)
            {
                return await Result<int>.FailureAsync($"Picklist with id: [{request.Id}] not found.");
            }
            item = _mapper.Map(request, item);
            item.AddDomainEvent(new UpdatedEvent<PicklistSet>(item));
            await _context.SaveChangesAsync(cancellationToken);
            return await Result<int>.SuccessAsync(item.Id);
        }
        else
        {
            var keyValue = _mapper.Map<PicklistSet>(request);
            keyValue.AddDomainEvent(new UpdatedEvent<PicklistSet>(keyValue));
            _context.PicklistSets.Add(keyValue);
            await _context.SaveChangesAsync(cancellationToken);
            return await Result<int>.SuccessAsync(keyValue.Id);
        }
    }
}