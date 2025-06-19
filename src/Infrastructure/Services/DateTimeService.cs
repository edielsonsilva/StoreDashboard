using StoreDashboard.Blazor.Application.Common.Interfaces;

namespace StoreDashboard.Blazor.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.UtcNow;
}