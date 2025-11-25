using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BloomAndRoot.Infrastructure.Identity;
using BloomAndRoot.Infrastructure.Interfaces;
using DotNetEnv;
using Microsoft.IdentityModel.Tokens;

namespace BloomAndRoot.Infrastructure.Services
{
  public class TokenService() : ITokenService
  {
    public string GenerateToken(ApplicationUser user, IList<string> roles)
    {
      Env.Load();

      var claims = new List<Claim>
      {
        new(ClaimTypes.NameIdentifier, user.Id),
        new(ClaimTypes.Email, user.Email!),
        new(ClaimTypes.Name, user.UserName!)
      };

      foreach (string role in roles)
      {
        claims.Add(new Claim(ClaimTypes.Role, role));
      }

      var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_SECRET_KEY")!));
      var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

      var token = new JwtSecurityToken(
        issuer: "BloomAndRoot",
        audience: "BloomAndRootUsers",
        claims,
        expires: DateTime.UtcNow.AddMinutes(10),
        signingCredentials: credentials
      );

      return new JwtSecurityTokenHandler().WriteToken(token);
    }
  }
}