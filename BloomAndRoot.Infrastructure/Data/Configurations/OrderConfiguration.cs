using BloomAndRoot.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BloomAndRoot.Infrastructure.Data.Configurations
{
  public class OrderConfiguration : IEntityTypeConfiguration<Order>
  {
    public void Configure(EntityTypeBuilder<Order> builder)
    {
      builder.ToTable("Orders");
      builder.HasKey((o) => o.Id);

      builder.HasOne((o) => o.Customer) // <- FK with Customer entity
        .WithMany((c) => c.Orders)
        .HasForeignKey((o) => o.CustomerId)
        .OnDelete(DeleteBehavior.Restrict);
      
      builder.Property((o) => o.TotalAmount)
        .IsRequired()
        .HasColumnType("decimal(18,2)");
      
      builder.Property((o) => o.Status)
        .IsRequired()
        .HasConversion<string>();
      
      builder.Property((o) => o.ShippingAddress)
        .IsRequired()
        .HasMaxLength(500);
      
      builder.Property((o) => o.CreatedAt)
        .IsRequired();

      builder.Property((o) => o.UpdatedAt)
        .IsRequired();

      builder.HasMany((o) => o.OrderItems) // <- relationship 1:N
        .WithOne((oi) => oi.Order)
        .HasForeignKey((oi) => oi.OrderId)
        .OnDelete(DeleteBehavior.Cascade);
    }
  }
}