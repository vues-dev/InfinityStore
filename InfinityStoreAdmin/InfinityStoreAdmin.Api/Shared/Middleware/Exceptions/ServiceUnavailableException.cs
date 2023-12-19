namespace InfinityStoreAdmin.Api.Shared.Middleware.Exceptions;

/// <summary>
/// Represents a 503 Service Unavailable error. Indicates temporary service unavailability.
/// Example: Thrown when a feature is disabled for maintenance.
/// </summary>
public class ServiceUnavailableException : Exception
{
    public ServiceUnavailableException(string message) : base(message) { }
}