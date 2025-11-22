using BloomAndRoot.Application.DTOs;
using BloomAndRoot.Application.Exceptions;
using BloomAndRoot.Application.Interfaces;
using BloomAndRoot.Application.Mappers;
using BloomAndRoot.Domain.Entities;

namespace BloomAndRoot.Application.Features.Orders.Commands.CreateOrder
{
  public class CreateOrderCommandHandler(IPlantRepository plantRepository, IOrderRepository orderRepository)
  {
    private readonly IPlantRepository _plantRepository = plantRepository;
    private readonly IOrderRepository _orderRepository = orderRepository;

    public async Task<OrderDTO> Handle(CreateOrderCommand command)
    {
      var orderItems = new List<OrderItem>();

      foreach (var item in command.Items)
      {
        var plant = await _plantRepository.GetByIdAsync(item.PlantId)
          ?? throw new NotFoundException($"plant with id: {item.PlantId} does not exist"); // <- plant to add to orderItem

        plant.ReduceStock(item.Quantity); // <- reduce stock

        var orderItem = new OrderItem( // <- orderItem for Order.OrderItems
          plant.Id,
          plant.Name,
          item.Quantity,
          plant.Price
        );

        orderItems.Add(orderItem);
      }

      var order = new Order(command.CustomerId, command.ShippingAddress, orderItems);

      await _orderRepository.AddAsync(order);
      await _orderRepository.SaveChangesAsync(); // <- saved all changed made, create orderItems list and new order + plant's stock reduction

      return order.ToDTO();
    }
  }
}