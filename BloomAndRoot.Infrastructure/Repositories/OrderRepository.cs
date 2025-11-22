using BloomAndRoot.Application.Interfaces;
using BloomAndRoot.Domain.Entities;
using BloomAndRoot.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BloomAndRoot.Infrastructure.Repositories
{
  public class OrderRepository(AppDbContext appDbContext) : IOrderRepository
  {
    private readonly AppDbContext _appDbContext = appDbContext;

    public async Task<(IEnumerable<Order> orders, int totalCount)> GetAllAsync(int page = 15, int pageSize = 15)
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

    public async Task SaveChangesAsync()
    {
      await _appDbContext.SaveChangesAsync();
    }
  }
}