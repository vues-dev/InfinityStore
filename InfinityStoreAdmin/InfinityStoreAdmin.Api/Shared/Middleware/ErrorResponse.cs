using FluentValidation;

namespace InfinityStoreAdmin.Api.Shared.Middleware;

public class ErrorResponse
{
    public IEnumerable<string> Messages { get; set; }
    public IEnumerable<string?> Details { get; set; }

    public ErrorResponse(Exception ex)
    {
        if (ex is ValidationException validationEx)
        {
            Messages = validationEx.Errors.Select(e => e.ErrorMessage);
            Details = validationEx.Errors.Select(e => e.ToString());
        }
        else
        {
            Messages = new List<string> { ex.Message };
            Details = new List<string?> { ex.StackTrace };
        }
    }
}