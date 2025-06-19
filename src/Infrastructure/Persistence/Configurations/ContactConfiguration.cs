using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreDashboard.Blazor.Domain.Entities;

namespace StoreDashboard.Blazor.Infrastructure.Persistence.Configurations;
#nullable disable
public class ContactConfiguration : IEntityTypeConfiguration<Contact>
{
    public void Configure(EntityTypeBuilder<Contact> builder)
    {
        builder.Property(t => t.Name).HasMaxLength(50).IsRequired();
        builder.Ignore(e => e.DomainEvents);
    }
}