namespace BloomAndRoot.Application.Common
{
  public class SortParams(PlantSortBy sortBy = PlantSortBy.Name, SortOrder sortOrder = SortOrder.Asc)
  {
    public PlantSortBy SortBy { get; set; } = sortBy;
    public SortOrder SortOrder { get; set; } = sortOrder;
  }
}