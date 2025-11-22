using BloomAndRoot.Domain.Entities;

namespace BloomAndRoot.Application.Interfaces
{
  public interface IOrderRepository
  {
    // Queries
    Task<(IEnumerable<Order> orders, int totalCount)> GetAllAsync(int page = 15, int pageSize = 15);

    // Persistance
    Task SaveChangesAsync();
  }
}