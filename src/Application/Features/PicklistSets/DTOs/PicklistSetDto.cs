using StoreDashboard.Blazor.Domain.Common.Enums;
using StoreDashboard.Blazor.Domain.Entities;

namespace StoreDashboard.Blazor.Application.Features.PicklistSets.DTOs;

[Description("Picklist")]
public class PicklistSetDto
{
    [Description("Id")] public int Id { get; set; }
    [Description("Name")] public Picklist Name { get; set; }
    [Description("Value")] public string? Value { get; set; }
    [Description("Text")] public string? Text { get; set; }
    [Description("Description")] public string? Description { get; set; }
    public TrackingState TrackingState { get; set; } = TrackingState.Unchanged;

    private class Mapping : Profile
    {
        public Mapping()
        {
                CreateMap<PicklistSet, PicklistSetDto>(MemberList.None).ReverseMap();
            }
    }
}