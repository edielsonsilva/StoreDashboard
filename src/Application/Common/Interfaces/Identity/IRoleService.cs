using StoreDashboard.Blazor.Application.Features.Identity.DTOs;

namespace StoreDashboard.Blazor.Application.Common.Interfaces.Identity;

public interface IRoleService
{
    List<ApplicationRoleDto> DataSource { get; }
    event Func<Task>? OnChange;
    void Initialize();
    void Refresh();
}