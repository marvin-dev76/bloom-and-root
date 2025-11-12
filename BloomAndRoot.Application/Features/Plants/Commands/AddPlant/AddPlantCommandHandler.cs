using BloomAndRoot.Application.DTOs;
using BloomAndRoot.Application.Interfaces;
using BloomAndRoot.Application.Mappers;
using BloomAndRoot.Domain.Entities;

namespace BloomAndRoot.Application.Features.Plants.Commands.AddPlant
{
  public class AddPlantCommandHandler(IPlantRepository plantRepository)
  {
    private readonly IPlantRepository _plantRepository = plantRepository;

    public async Task<PlantDTO> Handle(AddPlantCommand command)
    {
      var plant = new Plant(command.Name, command.Description, command.Price, command.Stock);
      await _plantRepository.AddAsync(plant);
      await _plantRepository.SaveChangesAsync();
      return plant.ToDTO();
    }
  }
}