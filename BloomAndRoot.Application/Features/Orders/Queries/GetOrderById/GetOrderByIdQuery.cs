namespace BloomAndRoot.Application.Features.Orders.Queries.GetOrderById
{
  public class GetOrderByIdQuery(int id)
  {
    public int Id { get; set; } = id;
  }
}