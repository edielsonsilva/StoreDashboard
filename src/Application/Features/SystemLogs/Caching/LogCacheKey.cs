using StoreDashboard.Blazor.Application.Common.FusionCache;

namespace StoreDashboard.Blazor.Application.Features.SystemLogs.Caching;

public static class SystemLogsCacheKey
{
    public const string GetAllCacheKey = "all-logs";
    public static string GetChartDataCacheKey(string parameters)
    {
            return $"GetChartDataCacheKey,{parameters}";
        }
    public static string GetPaginationCacheKey(string parameters)
    {
            return $"LogsTrailsWithPaginationQuery,{parameters}";
        }
    public static IEnumerable<string>? Tags => new string[] { "logs" };
    public static void Refresh()
    {
            FusionCacheFactory.RemoveByTags(Tags);
        }
}