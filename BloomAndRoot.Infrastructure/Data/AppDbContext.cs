using BloomAndRoot.Domain.Entities;
using BloomAndRoot.Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace BloomAndRoot.Infrastructure.Data
{
  public class AppDbContext(DbContextOptions options) : DbContext(options)
  {
    public DbSet<Plant> Plants { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);
      modelBuilder.ApplyConfiguration(new PlantConfiguration());
    }
  }
}