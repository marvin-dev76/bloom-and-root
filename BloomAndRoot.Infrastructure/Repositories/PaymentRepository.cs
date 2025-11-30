using BloomAndRoot.Application.Interfaces;
using BloomAndRoot.Domain.Entities;
using BloomAndRoot.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BloomAndRoot.Infrastructure.Repositories
{
  public class PaymentRepository(AppDbContext appDbContext) : IPaymentRepository
  {
    private readonly AppDbContext _appDbContext = appDbContext;

    public async Task<Payment?> GetByIdAsync(int id)
    {
      return await _appDbContext.Payments
        .Include((p) => p.Order)
        .FirstOrDefaultAsync((p) => p.Id == id);
    }

    public async Task<Payment?> GetByExternalTransactionIdAsync(string externalTransactionId)
    {
      return await _appDbContext.Payments
        .Include((p) => p.Order)
        .FirstOrDefaultAsync((p) => p.ExternalTransactionId == externalTransactionId);
    }

    public async Task<Payment?> GetByOrderId(int orderId)
    {
      return await _appDbContext.Payments
        .Include((p) => p.Order)
        .FirstOrDefaultAsync((p) => p.OrderId == orderId);
    }

    public async Task AddAsync(Payment payment)
    {
      await _appDbContext.Payments.AddAsync(payment);
    }

    public void Update(Payment payment)
    {
      _appDbContext.Payments.Update(payment);
    }

    public async Task SaveChangesAsync()
    {
      await _appDbContext.SaveChangesAsync();
    }
  }
}