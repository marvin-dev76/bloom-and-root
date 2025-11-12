namespace BloomAndRoot.Application.Exceptions
{
  public class NotFoundException(string message) : Exception(message) { }
  public class ValidationException(string message) : Exception(message) { }
  public class BusinessRuleException(string message) : Exception(message) { }
}