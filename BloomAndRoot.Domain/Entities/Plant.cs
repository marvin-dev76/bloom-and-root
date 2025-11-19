namespace BloomAndRoot.Domain.Entities
{
  public class Plant : BaseEntity
  {
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public string ImageURL { get; set; } = string.Empty;

    private Plant() { }

    public Plant(string name, string description, decimal price, int stock)
    {
      if (string.IsNullOrWhiteSpace(name))
        throw new ArgumentException("property name cannot be null or empty", nameof(name));
      if (string.IsNullOrWhiteSpace(description))
        throw new ArgumentException("property description cannot be null or empty", nameof(description));
      if (price <= 0)
        throw new ArgumentException("property price must be higher than 0");
      if (stock < 0)
        throw new ArgumentException("property stock cannot be negative");

      Name = name;
      Description = description;
      Price = price;
      Stock = stock;
      CreatedAt = DateTime.UtcNow;
      UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateName(string name)
    {
      if (string.IsNullOrWhiteSpace(name))
        throw new ArgumentException("property name cannot be null or empty", nameof(name));
      Name = name;
      UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateDescription(string description)
    {
      if (string.IsNullOrWhiteSpace(description))
        throw new ArgumentException("property name cannot be null or empty", nameof(description));
      Description = description;
      UpdatedAt = DateTime.UtcNow;
    }

    public void UpdatePrice(decimal price)
    {
      if (price <= 0)
        throw new ArgumentException("property price must be higher than 0", nameof(price));
      Price = price;
      UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateStock(int stock)
    {
      if (stock < 0)
        throw new ArgumentException("property stock cannot be negative", nameof(stock));
      Stock = stock;
      UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateImageURL(string imageURL)
    {
      if (string.IsNullOrWhiteSpace(imageURL))
        throw new ArgumentException("property imageURL cannot be null or empty", nameof(imageURL));

      ImageURL = imageURL;
      UpdatedAt = DateTime.UtcNow;
    }

    public void AddStock(int quantity)
    {
      if (quantity <= 0)
        throw new ArgumentException("stock quantity must be greater than 0", nameof(quantity));

      Stock += quantity;
      UpdatedAt = DateTime.UtcNow;
    }

    public void ReduceStock(int quantity)
    {
      if (quantity <= 0)
        throw new ArgumentException("stock quantity must be greater than 0", nameof(quantity));

      if (Stock < quantity)
        throw new InvalidOperationException($"Insuficient stock. Available: {Stock}, Requested: {quantity}");

      Stock -= quantity;
      UpdatedAt = DateTime.UtcNow;
    }
  }
}
