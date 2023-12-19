using System.Text.Json;
using FluentValidation;
using InfinityStoreAdmin.Api.Shared.Middleware.Exceptions;

namespace InfinityStoreAdmin.Api.Shared.Middleware
{
    public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var statusCode = DetermineStatusCode(ex);
            var errorResponse = new ErrorResponse(ex);

            logger.LogError($"An error occurred: {statusCode} - {string.Join("; ", errorResponse.Messages)}. Source: {ex.Source}");

            await WriteErrorResponseAsync(context, statusCode, errorResponse);
        }

        private int DetermineStatusCode(Exception ex)
        {
            return ex switch
            {
                BadRequestException => StatusCodes.Status400BadRequest,
                ValidationException => StatusCodes.Status400BadRequest, // Handle Fluent Validation exceptions
                UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
                ForbiddenException => StatusCodes.Status403Forbidden,
                NotFoundException => StatusCodes.Status404NotFound,
                MethodNotAllowedException => StatusCodes.Status405MethodNotAllowed,
                ConflictException => StatusCodes.Status409Conflict,
                ServiceUnavailableException => StatusCodes.Status503ServiceUnavailable,
                _ => StatusCodes.Status500InternalServerError
            };
        }

        private Task WriteErrorResponseAsync(HttpContext context, int statusCode, ErrorResponse errorResponse)
        {
            var result = JsonSerializer.Serialize(errorResponse);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            return context.Response.WriteAsync(result);
        }
    }
}
