using BloomAndRoot.Application.DTOs;
using BloomAndRoot.Application.Exceptions;
using BloomAndRoot.Application.Interfaces;
using BloomAndRoot.Application.Mappers;

namespace BloomAndRoot.Application.Features.Plants.Commands.UploadPlantImage
{
  public class UploadPlantImageCommandHandler(IPlantRepository plantRepository, IFileStorageService fileStorageService)
  {
    private readonly IPlantRepository _plantRepository = plantRepository;
    private readonly IFileStorageService _fileStorageService = fileStorageService;

    public async Task<PlantDTO> Handle(UploadPlantImageCommand command)
    {
      var plant = await _plantRepository.GetByIdAsync(command.PlantId) ?? throw new NotFoundException($"Plant with Id: {command.PlantId} does not exist");

      if (!string.IsNullOrWhiteSpace(plant.ImageURL))
      {
        await _fileStorageService.DeleteFileAsync(plant.ImageURL);
      }

      var imageURL = await _fileStorageService.SaveFileAsync(command.FileStream, command.FileName, "plants");

      plant.UpdateImageURL(imageURL);
      await _plantRepository.SaveChangesAsync();

      return plant.ToDTO();
    }
  }
}