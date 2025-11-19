namespace BloomAndRoot.Application.Features.Plants.Commands.UploadPlantImage
{
  public class UploadPlantImageCommand(int plantId, string fileName, Stream fileStream, string contentType)
  {
    public int PlantId { get; set; } = plantId;
    public string FileName { get; set; } = fileName;
    public Stream FileStream { get; set; } = fileStream;
    public string ContentType { get; set; } = contentType;
  }
}