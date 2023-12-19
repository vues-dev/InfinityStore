namespace InfinityStoreAdmin.Api.Shared.Middleware.Exceptions;

/// <summary>
/// Represents a 404 Not Found error. Applicable when a requested resource does not exist.
/// Example: Thrown when trying to access a user profile that doesn't exist.
/// </summary>
public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message) { }
}