using System.ComponentModel.DataAnnotations;

namespace BloomAndRoot.Application.DTOs
{
  public class CreateOrderDTO
  {
    [Required]
    [MaxLength(500)]
    public string ShippingAddress { get; set; } = string.Empty;

    [Required]
    [MinLength(1, ErrorMessage = "Order must have at least 1 item")]
    public List<CreateOrderItemDTO> Items { get; set; } = [];
  }

  public class CreateOrderItemDTO
  {
    [Required]
    [Range(1, int.MaxValue)]
    public int PlantId { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    public int Quantity { get; set; }
  }
}