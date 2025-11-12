using BloomAndRoot.Application.DTOs;
using BloomAndRoot.Application.Features.Plants.Commands.AddPlant;
using BloomAndRoot.Application.Features.Plants.Queries.GetAllPlants;
using BloomAndRoot.Application.Features.Plants.Queries.GetPlantById;
using Microsoft.AspNetCore.Mvc;

namespace BloomAndRoot.API.Controllers
{
  [ApiController]
  [Route("/api/plants")]
  public class PlantsController(
    GetAllPlantsQueryHandler getAllPlantsQueryHandler, GetPlantByIdQueryHandler getPlantByIdQueryHandler, AddPlantCommandHandler addPlantCommandHandler) : ControllerBase
  {
    private readonly GetAllPlantsQueryHandler _getAllPlantsQueryHandler = getAllPlantsQueryHandler;
    private readonly GetPlantByIdQueryHandler _getPlantByIdQueryHandler = getPlantByIdQueryHandler;
    private readonly AddPlantCommandHandler _addPlantCommandHandler = addPlantCommandHandler;

    // get all plants endpoint
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

    // get plant by Id enpoint
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
      try
      {
        var query = new GetPlantByIdQuery(id);
        var result = await _getPlantByIdQueryHandler.Handle(query);
        return Ok(result);
      }
      catch (KeyNotFoundException ex)
      {
        return NotFound(ex.Message);
      }
      catch (Exception)
      {
        return StatusCode(500, new { error = "Internal Server Error" });
      }
    }

    // post plant endpoint
    public async Task<IActionResult> Add([FromBody] AddPlantDTO dto)
    {
      try
      {
        var command = new AddPlantCommand(dto.Name, dto.Description, dto.Price, dto.Stock);
        var result = await _addPlantCommandHandler.Handle(command);
        return Ok(result);
      }
      catch (Exception)
      {
        return StatusCode(500, new { error = "Internal Server Error" });
      }
    }
  }
}