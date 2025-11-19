using BloomAndRoot.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BloomAndRoot.Infrastructure.Data.Configurations
{
  public class PlantConfiguration : IEntityTypeConfiguration<Plant>
  {
    public void Configure(EntityTypeBuilder<Plant> builder)
    {
      builder.ToTable("Plants");
      builder.HasKey((p) => p.Id);

      builder.Property((p) => p.Name)
        .IsRequired()
        .HasMaxLength(100);

      builder.Property((p) => p.Description)
        .IsRequired()
        .HasMaxLength(500);

      builder.Property((p) => p.Price)
        .IsRequired()
        .HasColumnType("decimal(18, 2)");

      builder.Property((p) => p.Stock)
        .IsRequired();

      builder.Property((p) => p.ImageURL)
        .IsRequired(false)
        .HasMaxLength(500);

      builder.Property((p) => p.CreatedAt)
        .IsRequired();

      builder.Property((p) => p.UpdatedAt)
        .IsRequired();
    }
  }
}