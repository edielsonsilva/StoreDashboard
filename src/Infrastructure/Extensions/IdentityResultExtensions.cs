using StoreDashboard.Blazor.Application.Common.Models;

namespace StoreDashboard.Blazor.Infrastructure.Extensions;

public static class IdentityResultExtensions
{
    public static Result ToApplicationResult(this IdentityResult result)
    {
            return result.Succeeded
                ? Result.Success()
                : Result.Failure(result.Errors.Select(e => e.Description).ToArray());
        }
}