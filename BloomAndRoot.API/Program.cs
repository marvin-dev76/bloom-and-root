using BloomAndRoot.Application.Interfaces;
using BloomAndRoot.Infrastructure.Data;
using BloomAndRoot.Infrastructure.Repositories;
using DotNetEnv;
using Microsoft.EntityFrameworkCore;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>((options) =>
{

  var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
  var serverVersion = ServerVersion.AutoDetect(connectionString);
  options.UseMySql(connectionString, serverVersion);
});
builder.Services.AddScoped<IPlantRepository, PlantRepository>();

var app = builder.Build();

app.UseHttpsRedirection();

app.Run();