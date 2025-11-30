using BloomAndRoot.Application.Exceptions;
using BloomAndRoot.Application.Interfaces;
using BloomAndRoot.Domain.Entities;
using BloomAndRoot.Infrastructure.Interfaces;
using Stripe;
using Stripe.Checkout;

namespace BloomAndRoot.Infrastructure.Services
{
  public class PaymentService(IOrderRepository orderRepository, IPaymentRepository paymentRepository) : IPaymentService
  {
    private readonly IOrderRepository _orderRepository = orderRepository;
    private readonly IPaymentRepository _paymentRepository = paymentRepository;

    public async Task<string> CreateCheckoutSessionAsync(int orderId, string successURL, string cancelURL)
    {
      StripeConfiguration.ApiKey = Environment.GetEnvironmentVariable("STRIPE_SECRET_KEY");
      var order = await _orderRepository.GetByIdAsync(orderId) ??
        throw new NotFoundException($"order with Id: {orderId} not found");

      var lineItems = order.OrderItems.Select((oi) => new SessionLineItemOptions
      {
        PriceData = new SessionLineItemPriceDataOptions
        {
          Currency = "usd",
          ProductData = new SessionLineItemPriceDataProductDataOptions
          {
            Name = oi.PlantName,
          },
          UnitAmount = (long)(oi.UnitPrice * 100) // <- to cents
        },
        Quantity = oi.Quantity
      }).ToList();

      var options = new SessionCreateOptions
      {
        PaymentMethodTypes = ["card"],
        LineItems = lineItems,
        Mode = "payment",
        SuccessUrl = $"{successURL}?session_id={{CHECKOUT_SESSION_ID}}",
        CancelUrl = cancelURL,
        Metadata = new Dictionary<string, string>
        {
          { "order_id", orderId.ToString() }
        }
      };

      var service = new SessionService();
      var session = await service.CreateAsync(options);

      var payment = new Payment(
        orderId,
        "Stripe",
        session.Id,
        order.TotalAmount
      );

      return session.Url;
    }

    public async Task HandlePaymentSucessAsync(string externalTransactionId)
    {
      var payment = await _paymentRepository.GetByExternalTransactionIdAsync(externalTransactionId) ??
        throw new NotFoundException($"payment with transaction Id: {externalTransactionId} not found");

      payment.MarkAsCompleted();

      var order = await _orderRepository.GetByIdAsync(payment.OrderId) ??
        throw new NotFoundException($"order with Id: {payment.OrderId} not found");
      
      order.MarkAsPaid();
      _orderRepository.Update(order);

      _paymentRepository.Update(payment);
      await _paymentRepository.SaveChangesAsync();
    }
  }
}