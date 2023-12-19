namespace InfinityStoreAdmin.Api.Shared.Middleware.Exceptions;

/// <summary>
/// Represents a 409 Conflict error. Suitable for request conflicts with server state.
/// Example: Thrown when attempting to register with an already used email.
/// </summary>
public class ConflictException : Exception
{
    public ConflictException(string message) : base(message) { }
}