using BloomAndRoot.Application.Interfaces;
using BloomAndRoot.Domain.Entities;
using BloomAndRoot.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BloomAndRoot.Infrastructure.Repositories
{
  public class PlantRepository(AppDbContext appDbContext) : IPlantRepository
  {
    private readonly AppDbContext _appDbContext = appDbContext;

    public async Task<IEnumerable<Plant>> GetAllAsync(string? search = null)
    {
      var query = _appDbContext.Plants.AsQueryable();
      if (!string.IsNullOrWhiteSpace(search))
      {
        query = query.Where((p) => p.Name.Contains(search));
      }
      return await query.ToListAsync();
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