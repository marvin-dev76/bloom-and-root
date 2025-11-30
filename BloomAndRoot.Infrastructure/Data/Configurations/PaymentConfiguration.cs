using BloomAndRoot.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BloomAndRoot.Infrastructure.Data.Configurations
{
  public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
  {
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
      builder.ToTable("Payments");
      builder.HasKey((p) => p.Id);

      builder.HasOne((p) => p.Order)
        .WithOne((o) => o.Payment)
        .HasForeignKey<Payment>((p) => p.OrderId)
        .OnDelete(DeleteBehavior.Cascade);
      
      builder.Property((p) => p.PaymentProvider)
        .IsRequired()
        .HasMaxLength(50);
      
      builder.Property((p) => p.ExternalTransactionId)
        .IsRequired()
        .HasMaxLength(200);
      
      builder.Property((p) => p.Amount)
        .IsRequired()
        .HasColumnType("decimal(18,2)");
      
      builder.Property((p) => p.Currency)
        .IsRequired()
        .HasMaxLength(10);
      
      builder.Property((p) => p.Status)
        .IsRequired()
        .HasMaxLength(50);
      
      builder.Property((p) => p.CompletedAt)
        .IsRequired(false);
      
      builder.Property((p) => p.CreatedAt)
        .IsRequired();
      
      builder.Property((p) => p.UpdatedAt)
        .IsRequired();
    }
  }
}