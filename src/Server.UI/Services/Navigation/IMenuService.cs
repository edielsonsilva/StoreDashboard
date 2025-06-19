using StoreDashboard.Blazor.Server.UI.Models.NavigationMenu;

namespace StoreDashboard.Blazor.Server.UI.Services.Navigation;

public interface IMenuService
{
    IEnumerable<MenuSectionModel> Features { get; }
}