using StoreDashboard.Blazor.Application.Common.FusionCache;

namespace StoreDashboard.Blazor.Application.Features.Documents.Caching;

public static class DocumentCacheKey
{
    public const string GetAllCacheKey = "all-documents";
    public static string GetStreamByIdKey(int id)
    {
            return $"GetStreamByIdKey:{id}";
        }
    public static string GetPaginationCacheKey(string parameters)
    {
            return $"DocumentsWithPaginationQuery,{parameters}";
        }
    public static IEnumerable<string>? Tags => new string[] { "document" };
    public static void Refresh()
    {
            FusionCacheFactory.RemoveByTags(Tags);
        }
}