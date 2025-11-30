using BloomAndRoot.Domain.Entities;

namespace BloomAndRoot.Application.Interfaces
{
  public interface IPaymentRepository
  {
    Task<Payment?> GetByIdAsync(int id);
    Task<Payment?> GetByExternalTransactionIdAsync(string externalTransactionId);
    Task<Payment?> GetByOrderId(int orderId);
    Task AddAsync(Payment payment);
    void Update(Payment payment);
    Task SaveChangesAsync();
  }
}