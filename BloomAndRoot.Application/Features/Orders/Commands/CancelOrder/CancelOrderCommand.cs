namespace BloomAndRoot.Application.Features.Orders.Commands.CancelOrder
{
  public class CancelOrderCommand(int id, string customerId, bool isAdmin)
  {
    public int OrderId { get; set; } = id;
    public string CustomerId { get; set; } = customerId;
    public bool IsAdmin { get; set; } = isAdmin;
  }
}