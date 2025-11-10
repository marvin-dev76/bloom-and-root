namespace BloomAndRoot.Application.Features.Plants.Queries.GetPlantById
{
  public class GetPlantByIdQuery(int id)
  {
    public int Id { get; set; } = id;
  }
}