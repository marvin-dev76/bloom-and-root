namespace BloomAndRoot.Application.Features.Orders.Commands.DeleteOrder
{
  public class DeleteOrderCommand(int orderId)
  {
    public int OrderId { get; set; } = orderId;
  }
}