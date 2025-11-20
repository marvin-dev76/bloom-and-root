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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);
      modelBuilder.ApplyConfiguration(new PlantConfiguration());
    }
  }
}