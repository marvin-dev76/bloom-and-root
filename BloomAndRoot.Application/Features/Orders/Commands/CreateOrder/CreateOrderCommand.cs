using BloomAndRoot.Domain.Entities;

namespace BloomAndRoot.Application.Features.Orders.Commands.CreateOrder
{
  public class CreateOrderCommand
  {
    public string CustomerId { get; set; } = string.Empty;
    public string ShippingAddress { get; set; } = string.Empty;
    public IEnumerable<CreateOrderItemCommand> Items { get; set; } = [];
  }

  public class CreateOrderItemCommand
  {
    public int PlantId { get; set; }
    public int Quantity { get; set; }
  }
}