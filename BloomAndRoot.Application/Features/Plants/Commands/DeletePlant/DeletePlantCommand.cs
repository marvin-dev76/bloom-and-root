namespace BloomAndRoot.Application.Features.Plants.Commands.DeletePlant
{
  public class DeletePlantCommand(int id)
  {
    public int Id { get; set; } = id;
  }
}