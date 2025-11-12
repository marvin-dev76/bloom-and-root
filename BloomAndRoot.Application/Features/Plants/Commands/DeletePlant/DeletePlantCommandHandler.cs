using BloomAndRoot.Application.Exceptions;
using BloomAndRoot.Application.Interfaces;

namespace BloomAndRoot.Application.Features.Plants.Commands.DeletePlant
{
  public class DeletePlantCommandHandler(IPlantRepository plantRepository)
  {
    private readonly IPlantRepository _plantRepository = plantRepository;

    public async Task Handle(DeletePlantCommand command)
    {
      var plant = await _plantRepository.GetByIdAsync(command.Id) ?? throw new NotFoundException($"Plant with Id: {command.Id} does not exist");
      _plantRepository.Delete(plant);
      await _plantRepository.SaveChangesAsync();
    }
  }
}