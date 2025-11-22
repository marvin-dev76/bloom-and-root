using BloomAndRoot.Application.Interfaces;
using BloomAndRoot.Domain.Entities;
using BloomAndRoot.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BloomAndRoot.Infrastructure.Repositories
{
  public class OrderRepository(AppDbContext appDbContext) : IOrderRepository
  {
    private readonly AppDbContext _appDbContext = appDbContext;

    public async Task AddAsync(Order order)
    {
      await _appDbContext.Orders.AddAsync(order);
    }

    public void DeleteAsync(Order order)
    {
      _appDbContext.Orders.Remove(order);
    }

    public async Task<(IEnumerable<Order> orders, int totalCount)> GetAllAsync(int page = 1, int pageSize = 15)
    {
      var query = _appDbContext.Orders
        .Include((o) => o.Customer)
        .Include((o) => o.OrderItems)
          .ThenInclude((oi) => oi.Plant)
        .OrderByDescending((o) => o.CreatedAt)
        .AsQueryable();

      var totalCount = await query.CountAsync();

      var orders = await query
        .Skip((page - 1) * pageSize)
        .Take(pageSize)
        .ToListAsync();

      return (orders, totalCount);
    }

    public async Task<(IEnumerable<Order> orders, int totalCount)> GetAllByIdCustomerAsync(string customerId, int page = 15, int pageSize = 15)
    {
      var query = _appDbContext.Orders
        .Include((o) => o.Customer)
        .Include((o) => o.OrderItems)
          .ThenInclude((oi) => oi.Plant)
        .Where((o) => o.CustomerId == customerId)
        .OrderByDescending((o) => o.CreatedAt)
        .AsQueryable();

      var totalCount = await query.CountAsync();

      var orders = await query
        .Skip((page - 1) * pageSize)
        .Take(pageSize)
        .ToListAsync();

      return (orders, totalCount);
    }

    public async Task<Order?> GetByIdAsync(int id)
    {
      return await _appDbContext.Orders
        .Include((o) => o.Customer)
        .Include((o) => o.OrderItems)
          .ThenInclude((oi) => oi.Plant)
        .FirstOrDefaultAsync((o) => o.Id == id);
    }

    public void Update(Order order)
    {
      _appDbContext.Orders.Update(order);
    }

    public async Task SaveChangesAsync()
    {
      await _appDbContext.SaveChangesAsync();
    }
  }
}