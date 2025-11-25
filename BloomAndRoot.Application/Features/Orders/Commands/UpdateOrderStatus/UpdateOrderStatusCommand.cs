namespace BloomAndRoot.Application.Features.Orders.Commands.UpdateOrderStatus
{
  public class UpdateOrderStatusCommand(int orderId, string status)
  {
    public int OrderId { get; set; } = orderId;
    public string Status { get; set; } = status;
  }
}