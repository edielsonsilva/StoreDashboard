using StoreDashboard.Blazor.Application.Common.Models;
using StoreDashboard.Blazor.Application.Common.Security;

namespace StoreDashboard.Blazor.Application.Features.Contacts.Specifications;

#nullable enable
/// <summary>
/// Specifies the different views available for the Contact list.
/// </summary>
public enum ContactListView
{
    [Description("All")]
    All,
    [Description("My")]
    My,
    [Description("Created Toady")]
    TODAY,
    [Description("Created within the last 30 days")]
    LAST_30_DAYS
}
/// <summary>
/// A class for applying advanced filtering options to Contact lists.
/// </summary>
public class ContactAdvancedFilter: PaginationFilter
{
    public ContactListView ListView { get; set; } = ContactListView.All;
    public UserProfile? CurrentUser { get; set; }
}