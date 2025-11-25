using BloomAndRoot.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BloomAndRoot.Infrastructure.Data.Configurations
{
  public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
  {
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
      builder.ToTable("OrderItems");
      builder.HasKey((oi) => oi.Id);

      builder.HasOne((oi) => oi.Plant)
        .WithMany()
        .HasForeignKey((oi) => oi.PlantId)
        .OnDelete(DeleteBehavior.Restrict);
      
      builder.Property((oi) => oi.PlantName)
        .HasMaxLength(100)
        .IsRequired();
      
      builder.Property((oi) => oi.Quantity)
        .IsRequired();
      
      builder.Property((oi) => oi.UnitPrice)
        .HasColumnType("decimal(18,2)")
        .IsRequired();

      builder.Property((oi) => oi.Subtotal)
        .HasColumnType("decimal(18,2)")
        .IsRequired();
      
      builder.Property((oi) => oi.CreatedAt)
        .IsRequired();

      builder.Property((oi) => oi.UpdatedAt)
        .IsRequired();
    }
  }
}