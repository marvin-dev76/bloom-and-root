namespace BloomAndRoot.Application.Features.Plants.Commands.AddPlant
{
  public class AddPlantCommand(string name, string description, decimal price, int stock)
  {
    public string Name { get; set; } = name;
    public string Description { get; set; } = description;
    public decimal Price { get; set; } = price;
    public int Stock { get; set; } = stock;
  }
}