using BloomAndRoot.Application.DTOs;
using BloomAndRoot.Application.Exceptions;
using BloomAndRoot.Application.Interfaces;
using BloomAndRoot.Application.Mappers;

namespace BloomAndRoot.Application.Features.Orders.Queries.GetOrderById
{
  public class GetOrderByIdQueryHandler(IOrderRepository orderRepository)
  {
    private readonly IOrderRepository _orderRepository = orderRepository;

    public async Task<OrderDTO> Handle(GetOrderByIdQuery query)
    {
      var order = await _orderRepository.GetByIdAsync(query.Id) ?? throw new NotFoundException($"Order with Id: {query.Id} does not exist");
      return order.ToDTO();
    }
  }
}