namespace BloomAndRoot.Application.Interfaces
{
  public interface IFileStorageService
  {
    Task<string> SaveFileAsync(Stream fileStream, string fileName, string folder);
    Task DeleteFileAsync(string fileURL);
  }
}