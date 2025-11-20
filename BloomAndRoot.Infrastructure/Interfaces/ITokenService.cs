using BloomAndRoot.Infrastructure.Identity;

namespace BloomAndRoot.Infrastructure.Interfaces
{
  public interface ITokenService
  {
    string GenerateToken(ApplicationUser user, IList<string> roles);
  }
}