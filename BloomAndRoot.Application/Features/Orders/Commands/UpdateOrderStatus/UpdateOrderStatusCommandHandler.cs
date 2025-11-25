using BloomAndRoot.Application.DTOs;
using BloomAndRoot.Application.Exceptions;
using BloomAndRoot.Application.Interfaces;
using BloomAndRoot.Application.Mappers;
using BloomAndRoot.Domain.Enums;

namespace BloomAndRoot.Application.Features.Orders.Commands.UpdateOrderStatus
{
  public class UpdateOrderStatusCommandHandler(IOrderRepository orderRepository)
  {
    private readonly IOrderRepository _orderRepository = orderRepository;

    public async Task<OrderDTO> Handle(UpdateOrderStatusCommand command)
    {
      var order = await _orderRepository.GetByIdAsync(command.OrderId)
        ?? throw new NotFoundException($"order with Id: {command.OrderId}");

      var status = Enum.TryParse<OrderStatus>(command.Status, true, out var parsedStatus);
      if (!status) throw new ValidationException($"Invalid status: {command.Status}");

      order.UpdateStatus(parsedStatus);

      _orderRepository.Update(order);
      await _orderRepository.SaveChangesAsync();

      return order.ToDTO();
    }
  }
}