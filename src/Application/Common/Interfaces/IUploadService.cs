using StoreDashboard.Blazor.Application.Common.Models;

namespace StoreDashboard.Blazor.Application.Common.Interfaces;

public interface IUploadService
{
    Task<string> UploadAsync(UploadRequest request);
    Task RemoveAsync(string filename);
}