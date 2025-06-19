using System.Text.Json;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreDashboard.Blazor.Application.Common.Interfaces.Serialization;
using StoreDashboard.Blazor.Domain.Entities;

namespace StoreDashboard.Blazor.Infrastructure.Persistence.Configurations;
#nullable disable
public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasIndex(x => x.Name).IsUnique();
        builder.Property(x=>x.Name).HasMaxLength(80).IsRequired();
        builder.Ignore(e => e.DomainEvents);
        builder.Property(e => e.Pictures)
            .HasConversion(
                v => JsonSerializer.Serialize(v, DefaultJsonSerializerOptions.Options),
                v => JsonSerializer.Deserialize<List<ProductImage>>(v, DefaultJsonSerializerOptions.Options),
                new ValueComparer<List<ProductImage>>(
                    (c1, c2) => c1.SequenceEqual(c2),
                    c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                    c => c.ToList()));
    }
}