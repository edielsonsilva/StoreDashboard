﻿#nullable enable
#nullable disable warnings

using StoreDashboard.Blazor.Application.Features.Contacts.Caching;
using StoreDashboard.Blazor.Application.Features.Contacts.DTOs;
using StoreDashboard.Blazor.Application.Common.Interfaces;
using StoreDashboard.Blazor.Application.Common.Interfaces.Caching;
using StoreDashboard.Blazor.Application.Common.Models;
using StoreDashboard.Blazor.Domain.Entities;

namespace StoreDashboard.Blazor.Application.Features.Contacts.Commands.AddEdit;

public class AddEditContactCommand : ICacheInvalidatorRequest<Result<int>>
{
    [Description("Id")]
    public int Id { get; set; }
    [Description("Name")]
    public string Name { get; set; }
    [Description("Description")]
    public string? Description { get; set; }
    [Description("Email")]
    public string? Email { get; set; }
    [Description("Phone number")]
    public string? PhoneNumber { get; set; }
    [Description("Country")]
    public string? Country { get; set; }


    public string CacheKey => ContactCacheKey.GetAllCacheKey;
    public IEnumerable<string>? Tags => ContactCacheKey.Tags;
    private class Mapping : Profile
    {
        public Mapping()
        {
                CreateMap<ContactDto, AddEditContactCommand>(MemberList.None);
                CreateMap<AddEditContactCommand, Contact>(MemberList.None);
            }
    }
}

public class AddEditContactCommandHandler : IRequestHandler<AddEditContactCommand, Result<int>>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _context;
    public AddEditContactCommandHandler(
        IMapper mapper,
        IApplicationDbContext context)
    {
            _mapper = mapper;
            _context = context;
        }
    public async Task<Result<int>> Handle(AddEditContactCommand request, CancellationToken cancellationToken)
    {
            if (request.Id > 0)
            {
                var item = await _context.Contacts.FindAsync(request.Id, cancellationToken);
                if (item == null)
                {
                    return await Result<int>.FailureAsync($"Contact with id: [{request.Id}] not found.");
                }
                item = _mapper.Map(request, item);
                // raise a update domain event
                item.AddDomainEvent(new ContactUpdatedEvent(item));
                await _context.SaveChangesAsync(cancellationToken);
                return await Result<int>.SuccessAsync(item.Id);
            }
            else
            {
                var item = _mapper.Map<Contact>(request);
                // raise a create domain event
                item.AddDomainEvent(new ContactCreatedEvent(item));
                _context.Contacts.Add(item);
                await _context.SaveChangesAsync(cancellationToken);
                return await Result<int>.SuccessAsync(item.Id);
            }

        }
}