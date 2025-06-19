using StoreDashboard.Blazor.Application.Common.FusionCache;

namespace StoreDashboard.Blazor.Application.Features.AuditTrails.Caching;

public static class AuditTrailsCacheKey
{
    public const string GetAllCacheKey = "all-audittrails";

    public static string GetPaginationCacheKey(string parameters)
    {
            return $"AuditTrailsWithPaginationQuery,{parameters}";
        }

    public static IEnumerable<string>? Tags => new string[] { "audittrail" };
    public static void Refresh()
    {
            FusionCacheFactory.RemoveByTags(Tags);
        }

}