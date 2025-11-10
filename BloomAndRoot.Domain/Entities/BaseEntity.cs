namespace BloomAndRoot.Domain.Entities
{
  public class BaseEntity
  {
    public int Id { get; private set; }
    public  DateTime CreatedAt { get; protected set; }
    public DateTime UpdatedAt { get; protected set; }
  }
}