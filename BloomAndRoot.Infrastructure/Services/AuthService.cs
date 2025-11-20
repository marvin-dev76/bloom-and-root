using BloomAndRoot.Application.DTOs;
using BloomAndRoot.Application.Interfaces;
using BloomAndRoot.Domain.Entities;
using BloomAndRoot.Infrastructure.Identity;
using BloomAndRoot.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace BloomAndRoot.Infrastructure.Services
{
  public class AuthService(
    UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager,
    ITokenService tokenService,
    ICustomerRepository customerRepository
  ) : IAuthService
  {
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly SignInManager<ApplicationUser> _signInManager = signInManager;
    private readonly ITokenService _tokenService = tokenService;
    private readonly ICustomerRepository _customerRepository = customerRepository;

    public async Task<AuthResponseDTO> RegisterAsync(RegisterDTO dto)
    {
      var existingUser = await _userManager.FindByEmailAsync(dto.Email);
      if (existingUser != null)
        throw new Application.Exceptions.ValidationException("email already registered"); // <- TODO: change ValicationException name

      var user = new ApplicationUser // <- ApplicationUser (for authentication only)
      {
        UserName = dto.Email,
        Email = dto.Email,
        EmailConfirmed = true // <- just for now
      };

      var result = await _userManager.CreateAsync(user, dto.Password);

      if (!result.Succeeded)
      {
        var errors = string.Join(", ", result.Errors.Select((e) => e.Description));
        throw new Application.Exceptions.ValidationException($"registration failed: {errors}");
      }

      await _userManager.AddToRoleAsync(user, "Customer");

      var customer = new Customer(
        user.Id,
        dto.FullName,
        dto.Phone ?? string.Empty,
        dto.Address ?? string.Empty
      );

      await _customerRepository.AddAsync(customer);
      await _customerRepository.SaveChangesAsync();

      var roles = await _userManager.GetRolesAsync(user);
      var token = _tokenService.GenerateToken(user, roles);

      return new AuthResponseDTO
      {
        Token = token,
        Email = user.Email,
        FullName = customer.FullName,
        Roles = roles
      };
    }

    public async Task<AuthResponseDTO> LoginAsync(LoginDTO dto)
    {
      var user = await _userManager.FindByEmailAsync(dto.Email) ??
        throw new Application.Exceptions.ValidationException("email already registered");

      var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
      if (!result.Succeeded)
        throw new Application.Exceptions.ValidationException("invalid email or password");

      var customer = await _customerRepository.GetByIdAsync(user.Id);

      var roles = await _userManager.GetRolesAsync(user);
      var token = _tokenService.GenerateToken(user, roles);

      return new AuthResponseDTO
      {
        Token = token,
        Email = user.Email!,
        FullName = customer?.FullName ?? user.Email!,
        Roles = roles
      };
    }
  }
}