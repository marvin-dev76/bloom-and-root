using BloomAndRoot.Application.Exceptions;
using BloomAndRoot.Application.Interfaces;

namespace BloomAndRoot.Application.Features.Orders.Commands.DeleteOrder
{
  public class DeleteOrderCommandHandler(IOrderRepository orderRepository)
  {
    private readonly IOrderRepository _orderRepository = orderRepository;

    public async Task Handle(DeleteOrderCommand command)
    {
      var order = await _orderRepository.GetByIdAsync(command.OrderId) ??
        throw new NotFoundException($"order with Id: {command.OrderId}");

      if (order.Status != Domain.Enums.OrderStatus.Cancelled)
        throw new BusinessRuleException("Only cancelled orders can be deleted.");

      _orderRepository.Delete(order);
      await _orderRepository.SaveChangesAsync();
    }
  }
}