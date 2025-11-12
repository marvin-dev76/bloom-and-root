namespace BloomAndRoot.Application.DTOs
{
  public class AddPlantDTO
  {
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Stock { get; set; }
  }
}