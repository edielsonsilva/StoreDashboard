using StoreDashboard.Blazor.Application.Common.Models;
using StoreDashboard.Blazor.Application.Common.Security;

namespace StoreDashboard.Blazor.Application.Features.SystemLogs.Specifications;

public enum SystemLogListView
{
    [Description("All")] 
    All,
    [Description("Created Today")] 
    TODAY,
    [Description("View of the last 30 days")]
    LAST_30_DAYS
}

public class SystemLogAdvancedFilter : PaginationFilter
{
    public UserProfile? CurrentUser { get; set; }
    public LogLevel? Level { get; set; }
    public SystemLogListView ListView { get; set; } = SystemLogListView.LAST_30_DAYS;
}