using BloomAndRoot.Application.DTOs;
using BloomAndRoot.Application.Interfaces;
using BloomAndRoot.Application.Mappers;

namespace BloomAndRoot.Application.Features.Plants.Queries.GetAllPlants
{
  public class GetAllPlantsQueryHandler(IPlantRepository plantRepository)
  {
    private readonly IPlantRepository _plantRepository = plantRepository;

    public async Task<IEnumerable<PlantDTO>> Handle(GetAllPlantsQuery query)
    {
      var plants = await _plantRepository.GetAllAsync();
      return plants.Select((p) => p.ToDTO());
    }
  }
}