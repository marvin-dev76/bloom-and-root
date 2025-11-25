using BloomAndRoot.Application.DTOs;
using BloomAndRoot.Application.Exceptions;
using BloomAndRoot.Application.Interfaces;
using BloomAndRoot.Application.Mappers;

namespace BloomAndRoot.Application.Features.Orders.Commands.CancelOrder
{
  public class CancelOrderCommandHandler(IOrderRepository orderRepository)
  {
    private readonly IOrderRepository _orderRepository = orderRepository;

    public async Task<OrderDTO> Handle(CancelOrderCommand command)
    {
      var order = await _orderRepository.GetByIdAsync(command.OrderId) ??
        throw new NotFoundException($"order with Id: {command.OrderId}");
      
      if (!command.IsAdmin && command.CustomerId != order.CustomerId)
        throw new ValidationException("You are not allowed to cancel this order");

      order.Cancel();
      await _orderRepository.SaveChangesAsync();

      return order.ToDTO();
    }
  }
}