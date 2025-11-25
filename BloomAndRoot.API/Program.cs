using System.Text;
using BloomAndRoot.API.Middleware;
using BloomAndRoot.API.Services;
using BloomAndRoot.Application.Features.Orders.Commands.CreateOrder;
using BloomAndRoot.Application.Features.Orders.Queries.GetAllOrders;
using BloomAndRoot.Application.Features.Orders.Queries.GetMyOrders;
using BloomAndRoot.Application.Features.Orders.Queries.GetOrderById;
using BloomAndRoot.Application.Features.Plants.Commands.AddPlant;
using BloomAndRoot.Application.Features.Plants.Commands.DeletePlant;
using BloomAndRoot.Application.Features.Plants.Commands.UpdatePlant;
using BloomAndRoot.Application.Features.Plants.Commands.UploadPlantImage;
using BloomAndRoot.Application.Features.Plants.Queries.GetAllPlants;
using BloomAndRoot.Application.Features.Plants.Queries.GetPlantById;
using BloomAndRoot.Application.Interfaces;
using BloomAndRoot.Infrastructure.Data;
using BloomAndRoot.Infrastructure.Identity;
using BloomAndRoot.Infrastructure.Interfaces;
using BloomAndRoot.Infrastructure.Repositories;
using BloomAndRoot.Infrastructure.Services;
using DotNetEnv;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

// 1. DbContext
builder.Services.AddDbContext<AppDbContext>((options) =>
{
  var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
  var serverVersion = ServerVersion.AutoDetect(connectionString);
  options.UseMySql(connectionString, serverVersion);
});

// 2. Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>((options) =>
{
  options.Password.RequireDigit = true;
  options.Password.RequireLowercase = true;
  options.Password.RequireUppercase = true;
  options.Password.RequireNonAlphanumeric = false;
  options.Password.RequiredLength = 6;
  options.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();

// 3. JWT Authentication
builder.Services.AddAuthentication((options) =>
{
  options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
  options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
  options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer((options) =>
{
  options.TokenValidationParameters = new TokenValidationParameters
  {
    ValidateIssuer = true,
    ValidateAudience = true,
    ValidateLifetime = true,
    ValidateIssuerSigningKey = true,
    ValidIssuer = "BloomAndRoot",
    ValidAudience = "BloomAndRootUsers",
    IssuerSigningKey = new SymmetricSecurityKey(
      Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_SECRET_KEY")!)
    )
  };
});

// Repositories and Services
builder.Services.AddScoped<IPlantRepository, PlantRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<GetAllPlantsQueryHandler>();
builder.Services.AddScoped<GetPlantByIdQueryHandler>();
builder.Services.AddScoped<AddPlantCommandHandler>();
builder.Services.AddScoped<UpdatePlantCommandHandler>();
builder.Services.AddScoped<DeletePlantCommandHandler>();
builder.Services.AddScoped<IFileStorageService, LocalFileStorageService>();
builder.Services.AddScoped<UploadPlantImageCommandHandler>();
builder.Services.AddScoped<GetAllOrdersQueryHandler>();
builder.Services.AddScoped<GetOrderByIdQueryHandler>();
builder.Services.AddScoped<GetMyOrdersQueryHandler>();
builder.Services.AddScoped<CreateOrderCommandHandler>();

builder.Services.AddControllers().AddJsonOptions((options) =>
{
  options.JsonSerializerOptions.NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.Strict;
});

var app = builder.Build();

// Seed
using (var scope = app.Services.CreateScope())
{
  var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
  var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
  await IdentitySeeder.SeedRolesAndAdminAsync(userManager, roleManager);
}

// Uploads
string uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
if (!Directory.Exists(uploadPath))
{
  Directory.CreateDirectory(uploadPath);
}

// Middleware
app.UseStaticFiles();
app.UseStaticFiles(new StaticFileOptions
{
  FileProvider = new PhysicalFileProvider(uploadPath),
  RequestPath = "/uploads"
});

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<GlobalExceptionMiddleware>();
app.MapControllers();

app.Run();