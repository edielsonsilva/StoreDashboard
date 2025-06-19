using StoreDashboard.Blazor.Domain.Identity;

namespace StoreDashboard.Blazor.Application.Features.Identity.DTOs;

[Description("Roles")]
public class ApplicationRoleDto
{
    [Description("Id")] public string Id { get; set; } = Guid.NewGuid().ToString();
    [Description("Name")] public string Name { get; set; } = string.Empty;
    [Description("Tenant Id")] public string? TenantId { get; set; }
    [Description("Tenant Name")] public string? TenantName { get; set; }
    [Description("Normalized Name")] public string? NormalizedName { get; set; }
    [Description("Description")] public string? Description { get; set; }
    private class Mapping : Profile
    {
        public Mapping()
        {
                CreateMap<ApplicationRole, ApplicationRoleDto>(MemberList.None)
                    .ForMember(x => x.TenantName, s => s.MapFrom(y => y.Tenant!.Name));
            }
    }
}