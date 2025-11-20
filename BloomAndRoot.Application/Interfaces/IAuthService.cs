using BloomAndRoot.Application.DTOs;

namespace BloomAndRoot.Application.Interfaces
{
  public interface IAuthService
  {
    Task<AuthResponseDTO> RegisterAsync(RegisterDTO dto);
    Task<AuthResponseDTO> LoginAsync(LoginDTO dto);
  }
}