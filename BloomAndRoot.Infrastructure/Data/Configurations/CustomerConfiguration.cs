using BloomAndRoot.Domain.Entities;
using BloomAndRoot.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BloomAndRoot.Infrastructure.Data.Configurations
{
  public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
  {
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
      builder.ToTable("Customers");
      builder.HasKey((c) => c.Id);

      builder.HasOne<ApplicationUser>() // <- relation 1:1 with ApplicationUser
        .WithOne()
        .HasForeignKey<Customer>((c) => c.Id)
        .OnDelete(DeleteBehavior.Cascade);
      
      builder.Property((c) => c.FullName)
        .IsRequired()
        .HasMaxLength(200);
      
      builder.Property((c) => c.Phone)
        .IsRequired(false)
        .HasMaxLength(20);
      
      builder.Property((c) => c.Address)
        .IsRequired(false)
        .HasMaxLength(500);
      
      builder.Property((c) => c.CreatedAt)
        .IsRequired();
      
      builder.Property((c) => c.UpdatedAt)
        .IsRequired();
    }
  }
}