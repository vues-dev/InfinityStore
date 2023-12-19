namespace InfinityStoreAdmin.Api.Shared.Middleware.Exceptions;

/// <summary>
/// Represents a 403 Forbidden error. Indicates server refusal due to business rules.
/// Example: Thrown when a user tries to access a restricted resource.
/// </summary>
public class ForbiddenException : Exception
{
    public ForbiddenException(string message) : base(message) { }
}