using StoreDashboard.Blazor.Domain.Common.Entities;
using StoreDashboard.Blazor.Domain.Entities;

namespace StoreDashboard.Blazor.Domain.Identity;

public class ApplicationRole : IdentityRole, IAuditableEntity
{
    public ApplicationRole()
    {
            RoleClaims = new HashSet<ApplicationRoleClaim>();
            UserRoles = new HashSet<ApplicationUserRole>();
        }

    public ApplicationRole(string roleName) : base(roleName)
    {
            RoleClaims = new HashSet<ApplicationRoleClaim>();
            UserRoles = new HashSet<ApplicationUserRole>();
        }
    public string? TenantId { get; set; }
    public virtual Tenant? Tenant { get; set; }
    public string? Description { get; set; }
    public virtual ICollection<ApplicationRoleClaim> RoleClaims { get; set; }
    public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
    public DateTime? Created { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? LastModified { get; set; }
    public string? LastModifiedBy { get; set; }

}