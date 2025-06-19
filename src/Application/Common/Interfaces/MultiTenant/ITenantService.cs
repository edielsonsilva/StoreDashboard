using StoreDashboard.Blazor.Application.Features.Tenants.DTOs;

namespace StoreDashboard.Blazor.Application.Common.Interfaces.MultiTenant;

public interface ITenantService
{
    List<TenantDto> DataSource { get; }
    event Func<Task>? OnChange;
    void Initialize();
    void Refresh();
}