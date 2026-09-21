using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace BuldingBlocks.Exceptions.Handler
{
    public class CustomExceptionHandler(ILogger<CustomExceptionHandler> logger) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
        {
            logger.LogError("Error Message: {message}, Time of occurrence: {time}", exception.Message
                , DateTime.UtcNow);

            (string Title, int StatusCode, string Detail) details = exception switch
            {
                InternalServerException => (
                exception.GetType().Name, 
                StatusCodes.Status500InternalServerError, 
                exception.Message),

                BadRequestException => (
                exception.GetType().Name,
                StatusCodes.Status400BadRequest,
                exception.Message
                ),

                NotFoundException => (
                exception.GetType().Name,
                StatusCodes.Status404NotFound,
                exception.Message
                ),

                FluentValidation.ValidationException => (
                exception.GetType().Name,
                StatusCodes.Status400BadRequest,
                exception.Message
                ),

                _ => (
                exception.GetType().Name,
                StatusCodes.Status500InternalServerError,
                exception.Message
                )
            };

            var problemDetails = new ProblemDetails
            {
                Title = details.Title,
                Status = details.StatusCode,
                Detail = details.Detail,
                Instance = context.Request.Path
            };

            problemDetails.Extensions.Add("traceId", context.TraceIdentifier);

            if(exception is FluentValidation.ValidationException validationException)
            {
                problemDetails.Extensions.Add("validationErrors", validationException.Errors);
            }

            await context.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }
    }
}
