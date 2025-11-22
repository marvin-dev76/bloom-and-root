using BloomAndRoot.Application.Common;
using BloomAndRoot.Application.DTOs;
using BloomAndRoot.Application.Interfaces;
using BloomAndRoot.Application.Mappers;

namespace BloomAndRoot.Application.Features.Orders.Queries.GetMyOrders
{
  public class GetMyOrdersQueryHandler(IOrderRepository orderRepository)
  {
    private readonly IOrderRepository _orderRepository = orderRepository;

    public async Task<PagedResult<OrderDTO>> Handle(GetMyOrdersQuery query)
    {
      var (orders, totalCount) = await _orderRepository.GetAllByIdCustomerAsync(query.CustomerId, query.Page, query.PageSize);

      return new PagedResult<OrderDTO>
      {
        Items = orders.Select((o) => o.ToDTO()),
        TotalCount = totalCount,
        Page = query.Page,
        PageSize = query.PageSize
      };
    }
  }
}