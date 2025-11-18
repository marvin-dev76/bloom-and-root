using BloomAndRoot.Application.Common;

namespace BloomAndRoot.Application.Features.Plants.Queries.GetAllPlants
{
  public class GetAllPlantsQuery(string? search, decimal? minPrice, decimal? maxPrice, SortParams sortParams, int page = 1, int pageSize = 15)
  {
    public string? Search { get; set; } = search;
    public decimal? MinPrice { get; set; } = minPrice;
    public decimal? MaxPrice { get; set; } = maxPrice;
    public SortParams SortParams { get; set; } = sortParams;
    public int Page { get; set; } = page > 0 ? page : 1;
    public int PageSize { get; set; } = pageSize > 0 && pageSize <= 100 ? pageSize : 15;
  }
}