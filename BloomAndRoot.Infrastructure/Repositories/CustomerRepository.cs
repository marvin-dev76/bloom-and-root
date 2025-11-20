using BloomAndRoot.Application.Interfaces;
using BloomAndRoot.Domain.Entities;
using BloomAndRoot.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BloomAndRoot.Infrastructure.Repositories
{
  public class CustomerRepository(AppDbContext appDbContext) : ICustomerRepository
  {
    private readonly AppDbContext _appDbContext = appDbContext;

    public async Task AddAsync(Customer customer)
    {
      await _appDbContext.Customers.AddAsync(customer);
    }

    public async Task<Customer?> GetByEmailAsync(string email)
    {
      var user = await _appDbContext.Users.FirstOrDefaultAsync((u) => u.Email == email);
      if (user == null) return null;

      return await _appDbContext.Customers.FirstOrDefaultAsync((c) => c.Id == user.Id);
    }

    public async Task<Customer?> GetByIdAsync(string id)
    {
      return await _appDbContext.Customers.FirstOrDefaultAsync((c) => c.Id == id);
    }

    public async Task SaveChangesAsync()
    {
      await _appDbContext.SaveChangesAsync();
    }

    public void Update(Customer customer)
    {
      _appDbContext.Customers.Update(customer);
    }
  }
}