using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreDashboard.Blazor.Domain.Entities;

namespace StoreDashboard.Blazor.Infrastructure.Persistence.Configurations;

public class TenantConfiguration : IEntityTypeConfiguration<Tenant>
{
    public void Configure(EntityTypeBuilder<Tenant> builder)
    {
            //builder.Ignore(e => e.DomainEvents);
        }
}