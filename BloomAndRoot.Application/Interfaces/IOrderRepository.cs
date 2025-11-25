using BloomAndRoot.Domain.Entities;

namespace BloomAndRoot.Application.Interfaces
{
  public interface IOrderRepository
  {
    // Queries
    Task<(IEnumerable<Order> orders, int totalCount)> GetAllAsync(int page = 1, int pageSize = 15);
    Task<(IEnumerable<Order> orders, int totalCount)> GetAllByIdCustomerAsync(string customerId, int page = 15, int pageSize = 15);
    Task<Order?> GetByIdAsync(int id);

    // Commands
    Task AddAsync(Order order);
    void Update(Order order);
    void Delete(Order order);

    // Persistance
    Task SaveChangesAsync();
  }
}