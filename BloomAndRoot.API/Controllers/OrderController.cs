using System.Security.Claims;
using BloomAndRoot.Application.DTOs;
using BloomAndRoot.Application.Features.Orders.Commands.CancelOrder;
using BloomAndRoot.Application.Features.Orders.Commands.CreateOrder;
using BloomAndRoot.Application.Features.Orders.Commands.UpdateOrderStatus;
using BloomAndRoot.Application.Features.Orders.Queries.GetAllOrders;
using BloomAndRoot.Application.Features.Orders.Queries.GetMyOrders;
using BloomAndRoot.Application.Features.Orders.Queries.GetOrderById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BloomAndRoot.API.Controllers
{
  [ApiController]
  [Route("api/orders")]
  public class OrderController(
    GetAllOrdersQueryHandler getAllOrdersQueryHandler,
    GetOrderByIdQueryHandler getOrderByIdQueryHandler,
    GetMyOrdersQueryHandler getMyOrdersQueryHandler,
    CreateOrderCommandHandler createOrderCommandHandler,
    UpdateOrderStatusCommandHandler updateOrderStatusCommandHandler,
    CancelOrderCommandHandler cancelOrderCommandHandler) : ControllerBase
  {
    private readonly GetAllOrdersQueryHandler _getAllOrdersQueryHandler = getAllOrdersQueryHandler;
    private readonly GetOrderByIdQueryHandler _getOrderByIdQueryHandler = getOrderByIdQueryHandler;
    private readonly GetMyOrdersQueryHandler _getMyOrdersQueryHandler = getMyOrdersQueryHandler;
    private readonly CreateOrderCommandHandler _createOrderCommandHandler = createOrderCommandHandler;
    private readonly UpdateOrderStatusCommandHandler _updateOrderStatusCommandHandler = updateOrderStatusCommandHandler;
    private readonly CancelOrderCommandHandler _cancelOrderCommandHandler = cancelOrderCommandHandler;

    // GET All orders endpoint (just an admin can see all the others from all the customers)
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 15)
    {
      var query = new GetAllOrdersQuery(page, pageSize);
      var result = await _getAllOrdersQueryHandler.Handle(query);
      return Ok(result);
    }

    // GET Order by Id endpoint (only an admin can see any order by Id, customers can see their own orders)
    [HttpGet("{id}")]
    public async Task<ActionResult> GetById(int id)
    {
      var query = new GetOrderByIdQuery(id);
      var result = await _getOrderByIdQueryHandler.Handle(query);

      var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

      if (!User.IsInRole("Admin") && userId != result.CustomerId)
      {
        return Forbid();
      }

      return Ok(result);
    }

    // GET My orders endpoint (customers can get their own orders paged)
    [HttpGet("my-orders")]
    public async Task<IActionResult> GetMyOrders([FromQuery] int page = 1, [FromQuery] int pageSize = 15)
    {
      var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

      if (string.IsNullOrWhiteSpace(userId))
        return Unauthorized(new { error = "user not authenticated" });

      var query = new GetMyOrdersQuery(userId, page, pageSize);
      var result = await _getMyOrdersQueryHandler.Handle(query);

      return Ok(result);
    }

    // POST Order endpoint
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

    // PUT update order status (only admin can update the order status)
    [HttpPut("{id}/status")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateOrderStatusDTO dto)
    {
      var command = new UpdateOrderStatusCommand(id, dto.Status);
      var result = await _updateOrderStatusCommandHandler.Handle(command);

      return Ok(result);
    }

    // PUT cancel order endpoint
    [HttpPut("{id}/cancel")]
    [Authorize]
    public async Task<IActionResult> Cancel(int id)
    {
      var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var isAdmin = User.IsInRole("Admin");

      if (string.IsNullOrWhiteSpace(userId))
        return Unauthorized(new { error = "user not authenticated" });

      var command = new CancelOrderCommand(id, userId, isAdmin);
      var result = await _cancelOrderCommandHandler.Handle(command);

      return Ok(result);
    }
  }
}