namespace BloomAndRoot.Application.DTOs
{
  public class UpdatePlantDTO
  {
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal? Price { get; set; }
    public int? Stock { get; set; }
  }
}