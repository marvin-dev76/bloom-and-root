namespace BloomAndRoot.Domain.Entities
{
  public class Customer
  {
    public string Id { get; set; } = string.Empty; // <- Same Id as the ApplicationUser
    public string FullName { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public ICollection<Order> Orders { get; set; } = []; // <- Relationship with Order entity
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    private Customer() { }

    public Customer(string id, string fullName, string phone, string address)
    {
      if (string.IsNullOrWhiteSpace(id))
        throw new ArgumentException("property id cannot be null or empty", nameof(id));
      if (string.IsNullOrWhiteSpace(fullName))
        throw new ArgumentException("property fullName cannot be null or empty", nameof(fullName));

      Id = id; // <- Same Id as ApplicationUser
      FullName = fullName;
      Phone = phone;
      Address = address;
      CreatedAt = DateTime.UtcNow;
    }

    public void UpdateProfile(string fullName, string phone, string address)
    {
      if (!string.IsNullOrEmpty(fullName))
        FullName = fullName;

      Phone = phone ?? Phone;
      Address = address ?? Address;
      UpdatedAt = DateTime.UtcNow;
    }
  }
}