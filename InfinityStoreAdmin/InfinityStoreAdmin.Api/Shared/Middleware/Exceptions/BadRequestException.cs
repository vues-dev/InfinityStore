namespace InfinityStoreAdmin.Api.Shared.Middleware.Exceptions;

/// <summary>
/// Represents a 400 Bad Request error. Used when client-provided data is invalid.
/// Example: Thrown when a required field is missing in a request.
/// </summary>
public class BadRequestException : Exception
{
    public BadRequestException(string message) : base(message) { }
}