using BloomAndRoot.Domain.Entities;

namespace BloomAndRoot.Application.DTOs
{
  public class OrderDTO
  {
    public int Id { get; set; }
    public string CustomerId { get; set; } = string.Empty;
    public string CustomerName { get; set; } = string.Empty;
    public decimal TotalAmount { get; set; }
    public string Status { get; set; } = string.Empty;
    public string ShippingAddress { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public IEnumerable<OrderItemDTO> OrderItems { get; set; } = [];
  }
}