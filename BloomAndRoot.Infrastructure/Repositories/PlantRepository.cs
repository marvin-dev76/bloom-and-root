using BloomAndRoot.Application.Interfaces;
using BloomAndRoot.Domain.Entities;
using BloomAndRoot.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BloomAndRoot.Infrastructure.Repositories
{
  public class PlantRepository(AppDbContext appDbContext) : IPlantRepository
  {
    private readonly AppDbContext _appDbContext = appDbContext;

    public async Task<(IEnumerable<Plant> plants, int totalCount)> GetAllAsync(
      string? search = null,
      decimal? minPrice = null,
      decimal? maxPrice = null,
      int page = 1,
      int pageSize = 15)
    {
      var query = _appDbContext.Plants.AsQueryable();

      if (!string.IsNullOrWhiteSpace(search))
      {
        query = query.Where((p) => p.Name.Contains(search));
      }

      if (minPrice.HasValue)
      {
        query = query.Where((p) => p.Price >= minPrice.Value);
      }

      if (maxPrice.HasValue)
      {
        query = query.Where((p) => p.Price <= maxPrice.Value);
      }

      var totalCount = await query.CountAsync();

      var plants = await query
        .OrderBy((p) => p.Name)
        .Skip((page - 1) * pageSize)
        .Take(pageSize)
        .ToListAsync();

      return (plants, totalCount);
    }

    public async Task<Plant?> GetByIdAsync(int id)
    {
      return await _appDbContext.Plants.FirstOrDefaultAsync((p) => p.Id == id);
    }

    public async Task AddAsync(Plant plant)
    {
      await _appDbContext.Plants.AddAsync(plant);
    }

    public void Update(Plant plant)
    {
      _appDbContext.Plants.Update(plant);
    }

    public void Delete(Plant plant)
    {
      _appDbContext.Plants.Remove(plant);
    }

    public async Task SaveChangesAsync()
    {
      await _appDbContext.SaveChangesAsync();
    }
  }
}