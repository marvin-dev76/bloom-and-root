using BloomAndRoot.Domain.Enums;

namespace BloomAndRoot.Domain.Entities
{
  public class Payment : BaseEntity
  {
    public int OrderId { get; set; }
    public string PaymentProvider { get; set; } = string.Empty; // <- Stripe, Wompi, etc
    public string ExternalTransactionId { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string Currency { get; set; } = "USD";
    public PaymentStatus Status { get; set; } = PaymentStatus.Pending;
    public DateTime? CompletedAt { get; set; }
    public Order Order { get; set; } = null!; // <- navigation property

    private Payment() { }

    public Payment(int orderId, string paymentProvider, string externalTransactionId, decimal amount, string currency = "USD")
    {
      if (orderId <= 0)
        throw new ArgumentException("property orderId must be greater than 0", nameof(orderId));
      if (string.IsNullOrWhiteSpace(paymentProvider))
        throw new ArgumentException("property paymentProvider cannot be null or empty", nameof(paymentProvider));
      if (string.IsNullOrWhiteSpace(externalTransactionId))
        throw new ArgumentException("property externalTransactionId cannot be null or empty", nameof(externalTransactionId));
      if (amount <= 0)
        throw new ArgumentException("property amount must be greater than 0", nameof(amount));
      
      OrderId =orderId;
      PaymentProvider = paymentProvider;
      ExternalTransactionId = externalTransactionId;
      Amount = amount;
      Status = PaymentStatus.Pending;
      CreatedAt = DateTime.UtcNow;
      UpdatedAt= DateTime.UtcNow;
    }

    public void MarkAsCompleted()
    {
      if (Status == PaymentStatus.Completed)
        throw new InvalidOperationException("payment is already completed");
      
      Status = PaymentStatus.Completed;
      UpdatedAt = DateTime.UtcNow;
      CompletedAt = DateTime.UtcNow;
    }

    public void MarkAsFailed()
    {
      if (Status == PaymentStatus.Completed)
        throw new InvalidOperationException("cannot mark as failed a completed payment");
      
      Status = PaymentStatus.Failed;
      UpdatedAt = DateTime.UtcNow;
    }
  }
}