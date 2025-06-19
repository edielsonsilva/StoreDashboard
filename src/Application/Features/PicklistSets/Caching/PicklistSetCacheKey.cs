using StoreDashboard.Blazor.Application.Common.FusionCache;

namespace StoreDashboard.Blazor.Application.Features.PicklistSets.Caching;

public static class PicklistSetCacheKey
{
    public const string GetAllCacheKey = "all-PicklistSet";
    public const string PicklistCacheKey = "all-PicklistSetcachekey";
    public static string GetCacheKey(string name)
    {
            return $"{name}-PicklistSet";
        }
    public static IEnumerable<string>? Tags => new string[] { "picklistset" };
    public static void Refresh()
    {
            FusionCacheFactory.RemoveByTags(Tags);
        }
}