using System.Text.Json;
using BloomAndRoot.Application.Exceptions;

namespace BloomAndRoot.API.Middleware
{
  public class GlobalExceptionMiddleware(RequestDelegate next)
  {
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
      try
      {
        await _next(context);
      }
      catch (NotFoundException ex)
      {
        context.Response.StatusCode = 404;
        await context.Response.WriteAsJsonAsync(new { message = ex.Message });
      }
      catch (ValidationException ex)
      {
        context.Response.StatusCode = 400;
        await context.Response.WriteAsJsonAsync(new { message = ex.Message });
      }
      catch (BusinessRuleException ex)
      {
        context.Response.StatusCode = 400;
        await context.Response.WriteAsJsonAsync(new { message = ex.Message });
      }
      catch (JsonException)
      {
        context.Response.StatusCode = 400;
        await context.Response.WriteAsJsonAsync(new { message = "Invalid Request Format" });
      }
      catch (Exception ex)
      {
        context.Response.StatusCode = 500;
        await context.Response.WriteAsJsonAsync(new { message = $"Internal Server Error: {ex.Message}" });
      }
    }
  }
}