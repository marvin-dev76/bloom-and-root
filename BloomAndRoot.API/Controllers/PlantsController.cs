using BloomAndRoot.Application.Features.Plants.Queries.GetAllPlants;
using Microsoft.AspNetCore.Mvc;

namespace BloomAndRoot.API.Controllers
{
  [ApiController]
  [Route("/api/plants")]
  public class PlantsController(GetAllPlantsQueryHandler getAllPlantsQueryHandler) : ControllerBase
  {
    private readonly GetAllPlantsQueryHandler _getAllPlantsQueryHandler = getAllPlantsQueryHandler;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
      try
      {
        var query = new GetAllPlantsQuery();
        var result = await _getAllPlantsQueryHandler.Handle(query);
        return Ok(result);
      }
      catch (Exception)
      {
        return StatusCode(500, new { error = "Internal Server Error" });
      }
    }
  }
}