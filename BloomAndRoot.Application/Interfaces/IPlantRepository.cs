using BloomAndRoot.Domain.Entities;

namespace BloomAndRoot.Application.Interfaces
{
  public interface IPlantRepository
  {
    // queries
    Task<IEnumerable<Plant>> GetAllAsync();
    Task<Plant?> GetByIdAsync(int id);

    // commands
    Task AddAsync(Plant plant);
    void Update(Plant plant);
    void Delete(Plant plant);

    // persistance
    Task SaveChangesAsync();
  }
}