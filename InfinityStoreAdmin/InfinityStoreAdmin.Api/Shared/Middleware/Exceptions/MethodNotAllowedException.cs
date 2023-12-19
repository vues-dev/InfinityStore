namespace InfinityStoreAdmin.Api.Shared.Middleware.Exceptions;

/// <summary>
/// Represents a 405 Method Not Allowed error. Used when a business operation is not allowed.
/// Example: Thrown when trying to delete a resource that should only be deactivated.
/// </summary>
public class MethodNotAllowedException : Exception
{
    public MethodNotAllowedException(string message) : base(message) { }
}