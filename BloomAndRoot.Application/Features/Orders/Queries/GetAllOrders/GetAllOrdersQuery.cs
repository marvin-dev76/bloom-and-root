namespace BloomAndRoot.Application.Features.Orders.Queries.GetAllOrders
{
  public class GetAllOrdersQuery(int page = 1, int pageSize = 15)
  {
    public int Page { get; set; } = page > 0 ? page : 1;
    public int PageSize { get; set; } = pageSize > 0 && pageSize <= 100 ? pageSize : 15;
  }
}