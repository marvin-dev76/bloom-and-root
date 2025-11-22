using BloomAndRoot.Application.Features.Orders.Queries.GetAllOrders;
using BloomAndRoot.Application.Features.Plants.Queries.GetAllPlants;
using Microsoft.AspNetCore.Mvc;

namespace BloomAndRoot.API.Controllers
{
  [ApiController]
  [Route("api/orders")]
  public class OrderController(GetAllOrdersQueryHandler getAllOrdersQueryHandler) : ControllerBase
  {
    private readonly GetAllOrdersQueryHandler _getAllOrdersQueryHandler = getAllOrdersQueryHandler;

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 15)
    {
      var query = new GetAllOrdersQuery(page, pageSize);
      var result = await _getAllOrdersQueryHandler.Handle(query);
      return Ok(result);
    }
  }
}