using BloomAndRoot.Domain.Entities;

namespace BloomAndRoot.Application.Interfaces
{
  public interface ICustomerRepository
  {
    Task<Customer?> GetByIdAsync(string id); 
    Task<Customer?> GetByEmailAsync(string email);
    Task AddAsync(Customer customer);
    void Update(Customer customer);
    Task SaveChangesAsync();
  }
}