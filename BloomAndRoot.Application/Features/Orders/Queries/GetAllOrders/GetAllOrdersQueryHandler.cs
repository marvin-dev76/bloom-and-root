using BloomAndRoot.Application.Common;
using BloomAndRoot.Application.DTOs;
using BloomAndRoot.Application.Interfaces;
using BloomAndRoot.Application.Mappers;

namespace BloomAndRoot.Application.Features.Orders.Queries.GetAllOrders
{
  public class GetAllOrdersQueryHandler(IOrderRepository orderRepository)
  {
    private readonly IOrderRepository _orderRepository = orderRepository;

    public async Task<PagedResult<OrderDTO>> Handle(GetAllOrdersQuery query)
    {
      var (orders, totalCount) = await _orderRepository.GetAllAsync(query.Page, query.PageSize);

      return new PagedResult<OrderDTO>
      {
        Items = [.. orders.Select((o) => o.ToDTO())],
        TotalCount = totalCount,
        Page = query.Page,
        PageSize = query.PageSize
      };
    }
  }
}