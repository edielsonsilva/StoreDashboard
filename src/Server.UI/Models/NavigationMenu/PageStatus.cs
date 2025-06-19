using System.ComponentModel;

namespace StoreDashboard.Blazor.Server.UI.Models.NavigationMenu;

public enum PageStatus
{
    [Description("Coming Soon")] ComingSoon,
    [Description("WIP")] Wip,
    [Description("New")] New,
    [Description("Completed")] Completed
}