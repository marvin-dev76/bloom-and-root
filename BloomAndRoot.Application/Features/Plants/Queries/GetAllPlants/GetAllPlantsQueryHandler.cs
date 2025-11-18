using BloomAndRoot.Application.Common;
using BloomAndRoot.Application.DTOs;
using BloomAndRoot.Application.Interfaces;
using BloomAndRoot.Application.Mappers;

namespace BloomAndRoot.Application.Features.Plants.Queries.GetAllPlants
{
  public class GetAllPlantsQueryHandler(IPlantRepository plantRepository)
  {
    private readonly IPlantRepository _plantRepository = plantRepository;

    public async Task<PagedResult<PlantDTO>> Handle(GetAllPlantsQuery query)
    {
      var (plants, totalCount) = await _plantRepository.GetAllAsync(query.SortParams, query.Search, query.MinPrice, query.MaxPrice, query.Page, query.PageSize);
      return new PagedResult<PlantDTO>
      {
        Items = plants.Select((p) => p.ToDTO()),
        TotalCount = totalCount,
        Page = query.Page,
        PageSize = query.PageSize
      };
    }
  }
}