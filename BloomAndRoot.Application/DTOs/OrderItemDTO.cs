namespace BloomAndRoot.Application.DTOs
{
  public class OrderItemDTO
  {
    public int Id { get; set; }
    public int PlantId { get; set; }
    public string PlantName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Subtotal { get; set; }
  }
}