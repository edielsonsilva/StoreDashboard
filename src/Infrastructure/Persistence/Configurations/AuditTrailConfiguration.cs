using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreDashboard.Blazor.Domain.Entities;
using StoreDashboard.Blazor.Infrastructure.Persistence.Conversions;

namespace StoreDashboard.Blazor.Infrastructure.Persistence.Configurations;
#nullable disable
public class AuditTrailConfiguration : IEntityTypeConfiguration<AuditTrail>
{
    public void Configure(EntityTypeBuilder<AuditTrail> builder)
    {
        builder.HasOne(x => x.Owner)
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.SetNull);
        builder.Navigation(e => e.Owner).AutoInclude();
        builder.Property(t => t.AuditType)
            .HasConversion<string>();
        builder.Property(e => e.AffectedColumns).HasJsonConversion();
        builder.Property(u => u.OldValues).HasJsonConversion();
        builder.Property(u => u.NewValues).HasJsonConversion();
        builder.Property(u => u.PrimaryKey).HasJsonConversion();
        builder.Ignore(x => x.TemporaryProperties);
        builder.Ignore(x => x.HasTemporaryProperties);
        builder.Property(x => x.DebugView).HasMaxLength(int.MaxValue);
        builder.Property(x => x.ErrorMessage).HasMaxLength(int.MaxValue);
    }
}