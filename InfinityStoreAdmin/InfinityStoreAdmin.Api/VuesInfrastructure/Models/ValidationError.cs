using System;
namespace Vues.Net.Models
{
    public class ValidationError
    {
        public required IDictionary<string, string[]> Errors { get; init; }
    }
}

