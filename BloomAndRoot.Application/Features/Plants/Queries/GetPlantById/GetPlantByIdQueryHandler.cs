using BloomAndRoot.Application.DTOs;
using BloomAndRoot.Application.Exceptions;
using BloomAndRoot.Application.Interfaces;
using BloomAndRoot.Application.Mappers;

namespace BloomAndRoot.Application.Features.Plants.Queries.GetPlantById
{
  public class GetPlantByIdQueryHandler(IPlantRepository plantRepository)
  {
    private readonly IPlantRepository _plantRepository = plantRepository;

    public async Task<PlantDTO> Handle(GetPlantByIdQuery query)
    {
      var plant = await _plantRepository.GetByIdAsync(query.Id) ?? throw new NotFoundException($"Plant with Id: {query.Id} does not exist");
      return plant.ToDTO();
    }
  }
}