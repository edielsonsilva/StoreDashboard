using StoreDashboard.Blazor.Application.Features.PicklistSets.DTOs;

namespace StoreDashboard.Blazor.Application.Common.Interfaces;

public interface IPicklistService
{
    List<PicklistSetDto> DataSource { get; }
    event Func<Task>? OnChange;
    void Initialize();
    void Refresh();
}