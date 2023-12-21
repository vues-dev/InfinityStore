namespace InfinityStoreAdmin.Api.VuesInfrastructure.Models
{
    public class ValidationError
    {
        public required IDictionary<string, string[]> Errors { get; init; }
    }
}

