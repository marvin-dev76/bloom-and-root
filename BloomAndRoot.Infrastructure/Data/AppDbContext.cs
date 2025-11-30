using BloomAndRoot.Domain.Entities;
using BloomAndRoot.Infrastructure.Data.Configurations;
using BloomAndRoot.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BloomAndRoot.Infrastructure.Data
{
  public class AppDbContext(DbContextOptions options) : IdentityDbContext<ApplicationUser>(options)
  {
    public DbSet<Plant> Plants { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Payment> Payments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);
      modelBuilder.ApplyConfiguration(new PlantConfiguration());
      modelBuilder.ApplyConfiguration(new CustomerConfiguration());
      modelBuilder.ApplyConfiguration(new OrderConfiguration());
      modelBuilder.ApplyConfiguration(new OrderItemConfiguration());
      modelBuilder.ApplyConfiguration(new PaymentConfiguration());
    }
  }
}