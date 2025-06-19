using StoreDashboard.Blazor.Application.Common.Extensions;
using StoreDashboard.Blazor.Application.Features.SystemLogs.Queries.PaginationQuery;
using StoreDashboard.Blazor.Domain.Entities;

namespace StoreDashboard.Blazor.Application.Features.SystemLogs.Specifications;
#nullable disable warnings
public class SystemLogAdvancedSpecification : Specification<SystemLog>
{
    public SystemLogAdvancedSpecification(SystemLogsWithPaginationQuery filter)
    {
        DateTime today = DateTime.UtcNow;
        var todayrange = today.GetDateRange("TODAY", filter.CurrentUser.LocalTimeOffset);
        var last30daysrange = today.GetDateRange("LAST_30_DAYS", filter.CurrentUser.LocalTimeOffset);
        // Build query conditions
        Query.Where(p => p.TimeStamp >= todayrange.Start && p.TimeStamp < todayrange.End.AddDays(1),
                filter.ListView == SystemLogListView.TODAY)
            .Where(p => p.TimeStamp >= last30daysrange.Start, filter.ListView == SystemLogListView.LAST_30_DAYS)
            .Where(p => p.Level == filter.Level.ToString(), filter.Level is not null)
            .Where(x =>x.Message.Contains(filter.Keyword),!string.IsNullOrEmpty(filter.Keyword));
    }
}