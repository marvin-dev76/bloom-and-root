using BloomAndRoot.Application.DTOs;
using BloomAndRoot.Domain.Entities;

namespace BloomAndRoot.Application.Mappers
{
  public static class OrderMapper
  {
    public static OrderDTO ToDTO(this Order order)
    {
      return new OrderDTO
      {
        Id = order.Id,
        CustomerId = order.CustomerId,
        CustomerName = order.Customer?.FullName ?? string.Empty,
        TotalAmount = order.TotalAmount,
        Status = order.Status.ToString(),
        ShippingAddress = order.ShippingAddress,
        CreatedAt = order.CreatedAt,
        UpdatedAt = order.UpdatedAt,
        OrderItems = [.. order.OrderItems.Select((oi) => oi.ToDTO())]
      };
    }

    public static OrderItemDTO ToDTO(this OrderItem orderItem)
    {
      return new OrderItemDTO
      {
        Id = orderItem.Id,
        PlantId = orderItem.PlantId,
        PlantName = orderItem.PlantName,
        Quantity = orderItem.Quantity,
        UnitPrice = orderItem.UnitPrice,
        Subtotal = orderItem.Subtotal
      };
    }
  }
}