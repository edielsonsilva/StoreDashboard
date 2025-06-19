using StoreDashboard.Blazor.Application.Common.Security;

namespace StoreDashboard.Blazor.Application.Features.Identity.Notifications;

public class UpdateUserProfileCommand : INotification
{
    public UserProfile UserProfile { get; set; } = null!;
}

public class UpdateUserProfileEventArgs : EventArgs
{
    public UserProfile UserProfile { get; set; } = null!;
}