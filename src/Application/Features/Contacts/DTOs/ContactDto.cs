﻿#nullable enable
#nullable disable warnings


using StoreDashboard.Blazor.Domain.Entities;

namespace StoreDashboard.Blazor.Application.Features.Contacts.DTOs;

[Description("Contacts")]
public class ContactDto
{
    [Description("Id")]
    public int Id { get; set; }
    [Description("Name")]
    public string Name { get; set; } = string.Empty;
    [Description("Description")]
    public string? Description {get;set;} 
    [Description("Email")]
    public string? Email {get;set;} 
    [Description("Phone number")]
    public string? PhoneNumber {get;set;} 
    [Description("Country")]
    public string? Country {get;set;} 


    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Contact, ContactDto>(MemberList.None);
            CreateMap<ContactDto, Contact>(MemberList.None)
            .ForMember(dest => dest.Created, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
            .ForMember(dest => dest.LastModified, opt => opt.Ignore())
            .ForMember(dest => dest.LastModifiedBy, opt => opt.Ignore())
            .ForMember(dest => dest.DomainEvents, opt => opt.Ignore());
        }
    }
}

