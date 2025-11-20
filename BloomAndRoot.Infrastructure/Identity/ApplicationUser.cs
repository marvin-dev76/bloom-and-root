using Microsoft.AspNetCore.Identity;

namespace BloomAndRoot.Infrastructure.Identity
{
  public class ApplicationUser : IdentityUser
  {
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
  }
}