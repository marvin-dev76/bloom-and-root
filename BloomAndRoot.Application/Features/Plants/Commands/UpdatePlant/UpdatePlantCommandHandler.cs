using BloomAndRoot.Application.DTOs;
using BloomAndRoot.Application.Interfaces;
using BloomAndRoot.Application.Mappers;

namespace BloomAndRoot.Application.Features.Plants.Commands.UpdatePlant
{
  public class UpdatePlantCommandHandler(IPlantRepository plantRepository)
  {
    private readonly IPlantRepository _plantRepository = plantRepository;

    public async Task<PlantDTO> Handle(UpdatePlantCommand command)
    {
      var plant = await _plantRepository.GetByIdAsync(command.Id) ?? throw new KeyNotFoundException($"Plant with Id: {command.Id} does not exist");

      if (command.Name != null)
        plant.UpdateName(command.Name);

      if (command.Description != null)
        plant.UpdateDescription(command.Description);

      if (command.Price.HasValue)
        plant.UpdatePrice(command.Price.Value);

      if (command.Stock.HasValue)
        plant.UpdateStock(command.Stock.Value);

      _plantRepository.Update(plant);
      await _plantRepository.SaveChangesAsync();
      return plant.ToDTO();
    }
  }
}