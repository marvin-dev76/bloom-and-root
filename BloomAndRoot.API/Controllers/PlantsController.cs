using BloomAndRoot.Application.DTOs;
using BloomAndRoot.Application.Features.Plants.Commands.AddPlant;
using BloomAndRoot.Application.Features.Plants.Commands.DeletePlant;
using BloomAndRoot.Application.Features.Plants.Commands.UpdatePlant;
using BloomAndRoot.Application.Features.Plants.Queries.GetAllPlants;
using BloomAndRoot.Application.Features.Plants.Queries.GetPlantById;
using Microsoft.AspNetCore.Mvc;

namespace BloomAndRoot.API.Controllers
{
  [ApiController]
  [Route("/api/plants")]
  public class PlantsController(
    GetAllPlantsQueryHandler getAllPlantsQueryHandler, GetPlantByIdQueryHandler getPlantByIdQueryHandler, AddPlantCommandHandler addPlantCommandHandler, UpdatePlantCommandHandler updatePlantCommandHandler, DeletePlantCommandHandler deletePlantCommandHandler) : ControllerBase
  {
    private readonly GetAllPlantsQueryHandler _getAllPlantsQueryHandler = getAllPlantsQueryHandler;
    private readonly GetPlantByIdQueryHandler _getPlantByIdQueryHandler = getPlantByIdQueryHandler;
    private readonly AddPlantCommandHandler _addPlantCommandHandler = addPlantCommandHandler;
    private readonly UpdatePlantCommandHandler _updatePlantCommandHandler = updatePlantCommandHandler;
    private readonly DeletePlantCommandHandler _deletePlantCommandHandler = deletePlantCommandHandler;

    // get all plants endpoint
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string? search, [FromQuery] int page = 1, [FromQuery] int pageSize = 15)
    {
      var query = new GetAllPlantsQuery(search, page, pageSize);
      var result = await _getAllPlantsQueryHandler.Handle(query);
      return Ok(result);
    }

    // get plant by Id enpoint
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
      var query = new GetPlantByIdQuery(id);
      var result = await _getPlantByIdQueryHandler.Handle(query);
      return Ok(result);
    }

    // post plant endpoint
    public async Task<IActionResult> Add([FromBody] AddPlantDTO dto)
    {
      var command = new AddPlantCommand(dto.Name, dto.Description, dto.Price, dto.Stock);
      var result = await _addPlantCommandHandler.Handle(command);
      return Ok(result);
    }

    // patch plant endpoint
    [HttpPatch("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdatePlantDTO dto)
    {
      var command = new UpdatePlantCommand(id, dto.Name, dto.Description, dto.Price, dto.Stock);
      var result = await _updatePlantCommandHandler.Handle(command);
      return Ok(result);
    }

    // delete plant endpoint
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
      var command = new DeletePlantCommand(id);
      await _deletePlantCommandHandler.Handle(command);
      return NoContent();
    }
  }
}