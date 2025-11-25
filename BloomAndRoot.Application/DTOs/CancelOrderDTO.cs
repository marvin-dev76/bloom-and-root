namespace BloomAndRoot.Application.DTOs
{
  public class CancelOrderDTO
  {
    public int OrderId { get; set; }
    public string CustomerId { get; set; } = string.Empty;
    public bool IsAdmin { get; set; }
  }
}