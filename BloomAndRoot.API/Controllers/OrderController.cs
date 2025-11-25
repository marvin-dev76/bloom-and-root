using System.Security.Claims;
using BloomAndRoot.Application.DTOs;
using BloomAndRoot.Application.Features.Orders.Commands.CreateOrder;
using BloomAndRoot.Application.Features.Orders.Queries.GetAllOrders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BloomAndRoot.API.Controllers
{
  [ApiController]
  [Route("api/orders")]
  public class OrderController(
    GetAllOrdersQueryHandler getAllOrdersQueryHandler,
    CreateOrderCommandHandler createOrderCommandHandler) : ControllerBase
  {
    private readonly GetAllOrdersQueryHandler _getAllOrdersQueryHandler = getAllOrdersQueryHandler;
    private readonly CreateOrderCommandHandler _createOrderCommandHandler = createOrderCommandHandler;

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 15)
    {
      var query = new GetAllOrdersQuery(page, pageSize);
      var result = await _getAllOrdersQueryHandler.Handle(query);
      return Ok(result);
    }

    [HttpPost]
    [Authorize(Roles = "Customer")]
    public async Task<IActionResult> Add([FromBody] CreateOrderDTO dto)
    {
      var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      if (string.IsNullOrWhiteSpace(userId))
        return Unauthorized(new { error = "user not authenticated" });

      var command = new CreateOrderCommand
      {
        CustomerId = userId,
        ShippingAddress = dto.ShippingAddress,
        Items = [.. dto.Items.Select((item) => new CreateOrderItemCommand
        {
          PlantId = item.PlantId,
          Quantity = item.Quantity
        })]
      };

      var result = await _createOrderCommandHandler.Handle(command);
      return Ok(result);
    }
  }
}