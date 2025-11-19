using BloomAndRoot.Application.Interfaces;

namespace BloomAndRoot.API.Services
{
  public class LocalFileStorageService : IFileStorageService
  {
    private readonly string _uploadPath;

    public LocalFileStorageService()
    {
      _uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
    }

    public async Task<string> SaveFileAsync(Stream fileStream, string fileName, string folder)
    {
      var folderPath = Path.Combine(_uploadPath, folder);

      if (!Directory.Exists(folderPath))
      {
        Directory.CreateDirectory(folderPath);
      }

      var uniqueFileName = $"{Guid.NewGuid()}_{fileName}";
      var filePath = Path.Combine(folderPath, uniqueFileName);

      using var fileStreamOutput = new FileStream(filePath, FileMode.Create);
      await fileStream.CopyToAsync(fileStreamOutput);

      return $"/uploads/{folder}/{uniqueFileName}";
    }

    public Task DeleteFileAsync(string fileURL)
    {
      if (string.IsNullOrWhiteSpace(fileURL))
        return Task.CompletedTask;

      var fileName = fileURL.Replace("/uploads/", "");
      var filePath = Path.Combine(_uploadPath, fileName);

      if (File.Exists(filePath))
      {
        File.Delete(filePath);
      }

      return Task.CompletedTask;
    }
  }
}