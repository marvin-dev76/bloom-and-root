using BloomAndRoot.Application.DTOs;
using BloomAndRoot.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BloomAndRoot.API.Controllers
{
  [ApiController]
  [Route("api/auth")]
  public class AuthController(IAuthService authService) : ControllerBase
  {
    private readonly IAuthService _authService = authService;

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDTO dto)
    {
      var result = await _authService.RegisterAsync(dto);
      return Ok(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDTO dto)
    {
      var result = await _authService.LoginAsync(dto);
      return Ok(result);
    }
  }
}