using BloomAndRoot.API.Middleware;
using BloomAndRoot.API.Services;
using BloomAndRoot.Application.Features.Plants.Commands.AddPlant;
using BloomAndRoot.Application.Features.Plants.Commands.DeletePlant;
using BloomAndRoot.Application.Features.Plants.Commands.UpdatePlant;
using BloomAndRoot.Application.Features.Plants.Commands.UploadPlantImage;
using BloomAndRoot.Application.Features.Plants.Queries.GetAllPlants;
using BloomAndRoot.Application.Features.Plants.Queries.GetPlantById;
using BloomAndRoot.Application.Interfaces;
using BloomAndRoot.Infrastructure.Data;
using BloomAndRoot.Infrastructure.Identity;
using BloomAndRoot.Infrastructure.Repositories;
using DotNetEnv;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>((options) =>
{
  var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
  var serverVersion = ServerVersion.AutoDetect(connectionString);
  options.UseMySql(connectionString, serverVersion);
});
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
builder.Services.AddScoped<IPlantRepository, PlantRepository>();
builder.Services.AddScoped<GetAllPlantsQueryHandler>();
builder.Services.AddScoped<GetPlantByIdQueryHandler>();
builder.Services.AddScoped<AddPlantCommandHandler>();
builder.Services.AddScoped<UpdatePlantCommandHandler>();
builder.Services.AddScoped<DeletePlantCommandHandler>();
builder.Services.AddScoped<IFileStorageService, LocalFileStorageService>();
builder.Services.AddScoped<UploadPlantImageCommandHandler>();
builder.Services.AddControllers().AddJsonOptions((options) =>
{
  options.JsonSerializerOptions.NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.Strict;
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
  var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
  var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

  await IdentitySeeder.SeedRolesAndAdminAsync(userManager, roleManager);
}

string uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "uploads");

if (!Directory.Exists(uploadPath))
{
  Directory.CreateDirectory(uploadPath);
}

app.UseStaticFiles();
app.UseStaticFiles(new StaticFileOptions
{
  FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "uploads")),
  RequestPath = "/uploads"
});
app.UseMiddleware<GlobalExceptionMiddleware>();
app.UseHttpsRedirection();
app.MapControllers();

app.Run();
