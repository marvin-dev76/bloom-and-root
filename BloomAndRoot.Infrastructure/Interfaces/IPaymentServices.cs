namespace BloomAndRoot.Infrastructure.Interfaces
{
  public interface IPaymentService
  {
    Task<string> CreateCheckoutSessionAsync(int orderId, string successURL, string cancelURL);
    Task HandlePaymentSucessAsync(string externalTransactionId);
  }
}