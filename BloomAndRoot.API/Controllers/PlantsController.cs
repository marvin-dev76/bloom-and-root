using BloomAndRoot.Application.Common;
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

    // GET all plants endpoint
    [HttpGet]
    public async Task<IActionResult> GetAll(
      [FromQuery] string? search,
      [FromQuery] decimal? minPrice,
      [FromQuery] decimal? maxPrice,
      [FromQuery] string? sortBy,
      [FromQuery] string? sortOrder,
      [FromQuery] int page = 1,
      [FromQuery] int pageSize = 15
      )
    {

      var sortByEnum = PlantSortBy.Name;
      if (!string.IsNullOrWhiteSpace(sortBy) && Enum.TryParse<PlantSortBy>(sortBy.ToLower(), ignoreCase: true, out var parsedSortBy))
      {
        sortByEnum = parsedSortBy;
      }

      var sortOrderEnum = SortOrder.Asc;
      if (!string.IsNullOrWhiteSpace(sortOrder) && Enum.TryParse<SortOrder>(sortOrder.ToLower(), ignoreCase: true, out var parsedSortOrder))
      {
        sortOrderEnum = parsedSortOrder;
      }

      var sortParams = new SortParams(sortByEnum, sortOrderEnum);

      var query = new GetAllPlantsQuery(search, minPrice, maxPrice, sortParams, page, pageSize);
      var result = await _getAllPlantsQueryHandler.Handle(query);
      return Ok(result);
    }

    // GET plant by Id enpoint
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
      var query = new GetPlantByIdQuery(id);
      var result = await _getPlantByIdQueryHandler.Handle(query);
      return Ok(result);
    }

    // POST plant endpoint
    public async Task<IActionResult> Add([FromBody] AddPlantDTO dto)
    {
      var command = new AddPlantCommand(dto.Name, dto.Description, dto.Price, dto.Stock);
      var result = await _addPlantCommandHandler.Handle(command);
      return Ok(result);
    }

    // PATCH plant endpoint
    [HttpPatch("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdatePlantDTO dto)
    {
      var command = new UpdatePlantCommand(id, dto.Name, dto.Description, dto.Price, dto.Stock);
      var result = await _updatePlantCommandHandler.Handle(command);
      return Ok(result);
    }

    // DELETE plant endpoint
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
      var command = new DeletePlantCommand(id);
      await _deletePlantCommandHandler.Handle(command);
      return NoContent();
    }
  }
}