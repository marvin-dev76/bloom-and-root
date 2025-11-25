using BloomAndRoot.Domain.Enums;

namespace BloomAndRoot.Domain.Entities
{
  public class Order : BaseEntity
  {
    public string CustomerId { get; set; } = string.Empty; // <- FK to Customer
    public decimal TotalAmount { get; set; }
    public OrderStatus Status { get; set; }
    public string ShippingAddress { get; set; } = string.Empty;
    public Customer Customer { get; set; } = null!; // <- Navigation property
    public ICollection<OrderItem> OrderItems { get; set; } = [];

    private Order() { }

    public Order(string customerId, string shippingAddress, IEnumerable<OrderItem> orderItems)
    {
      if (string.IsNullOrWhiteSpace(customerId))
        throw new ArgumentException("property customerId cannot be null or empty", nameof(customerId));
      if (string.IsNullOrWhiteSpace(shippingAddress))
        throw new ArgumentException("property shippingAddress cannot be null or empty", nameof(shippingAddress));
      if (!orderItems.Any())
        throw new ArgumentException("Order must have at least one item", nameof(orderItems));

      CustomerId = customerId;
      Status = OrderStatus.Pending;
      ShippingAddress = shippingAddress;
      OrderItems = [.. orderItems];
      TotalAmount = CalculateTotal();
      CreatedAt = DateTime.UtcNow;
      UpdatedAt = DateTime.UtcNow;
    }

    private decimal CalculateTotal()
    {
      return OrderItems.Sum((item) => item.Subtotal);
    }

    public void UpdateStatus(OrderStatus newStatus)
    {
      if (Status == OrderStatus.Cancelled)
        throw new InvalidOperationException("cannot update a cancelled order");
      if (Status == OrderStatus.Delivered)
        throw new InvalidOperationException("cannot update a delivered order");
      
      Status = newStatus;
      UpdatedAt = DateTime.UtcNow;
    }

    public void Cancel()
    {
      if (Status == OrderStatus.Cancelled)
        throw new InvalidOperationException("cannot cancel a cancelled order");
      if (Status == OrderStatus.Delivered)
        throw new InvalidOperationException("cannot cancel a delivered order");
      
      Status = OrderStatus.Cancelled;
      UpdatedAt = DateTime.UtcNow;
    }
  }

}