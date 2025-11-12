using System.ComponentModel.DataAnnotations;
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
      try
      {
        var plant = new Plant(command.Name, command.Description, command.Price, command.Stock);
        await _plantRepository.AddAsync(plant);
        await _plantRepository.SaveChangesAsync();
        return plant.ToDTO();
      }
      catch (ArgumentException ex)
      {
        throw new ValidationException(ex.Message);
      }

    }
  }
}