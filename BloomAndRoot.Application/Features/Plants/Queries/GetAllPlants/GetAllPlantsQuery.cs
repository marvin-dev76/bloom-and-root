namespace BloomAndRoot.Application.Features.Plants.Queries.GetAllPlants
{
  public class GetAllPlantsQuery(string? search)
  {
    public string? Search { get; set; } = search;
  }
}