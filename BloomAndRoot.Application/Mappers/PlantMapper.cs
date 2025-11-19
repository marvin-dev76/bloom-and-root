using BloomAndRoot.Application.DTOs;
using BloomAndRoot.Domain.Entities;

namespace BloomAndRoot.Application.Mappers
{
  public static class PlantMapper
  {
    public static Plant ToPlant(this PlantDTO plantDTO)
    {
      return new Plant(plantDTO.Name, plantDTO.Description, plantDTO.Price, plantDTO.Stock);
    }

    public static PlantDTO ToDTO(this Plant plant)
    {
      return new PlantDTO { Id = plant.Id, Name = plant.Name, Description = plant.Description, Price = plant.Price, Stock = plant.Stock, ImageURL = plant.ImageURL, CreatedAt = plant.CreatedAt, UpdatedAt = plant.UpdatedAt };
    }
  }
}