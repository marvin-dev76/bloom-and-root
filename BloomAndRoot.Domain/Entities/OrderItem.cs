namespace BloomAndRoot.Domain.Entities
{
  public class OrderItem : BaseEntity
  {
    public int OrderId { get; set; }
    public int PlantId { get; set; }
    public string PlantName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Subtotal { get; set; }
    public Order Order { get; set; } = null!;
    public Plant Plant { get; set; } = null!;

    private OrderItem() { }

    public OrderItem(int plantId, string plantName, int quantity, decimal unitPrice)
    {
      if (plantId <= 0)
        throw new ArgumentException("property plantId must be greated than 0", nameof(plantId));
      if (string.IsNullOrWhiteSpace(plantName))
        throw new ArgumentException("property plantName cannot be null or empty", nameof(plantName));
      if (quantity <= 0)
        throw new ArgumentException("property quantity must be greater than 0", nameof(quantity));
      if (unitPrice <= 0)
        throw new ArgumentException("property unitPrice must be greater than 0", nameof(unitPrice));
      
      PlantId = plantId;
      PlantName = plantName;
      Quantity = quantity;
      UnitPrice = unitPrice;
      Subtotal = quantity * unitPrice;
      CreatedAt = DateTime.UtcNow;
      UpdatedAt = DateTime.UtcNow;
    }
  }
}