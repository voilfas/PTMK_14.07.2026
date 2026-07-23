using Microsoft.AspNetCore.Mvc;
using TicketService.Domain.Common;

namespace TicketService.API.Common;

public static class ProblemDetailsFactory
{
    public static ProblemDetails Create(Error error)
    {
        var statusCode = ErrorMapper.GetStatusCode(error);

        return new ProblemDetails
        {
            Status = statusCode,
            Title = GetTitle(statusCode),
            Detail = error.Message,
            Type = error.Code
        };
    }

    private static string GetTitle(int statusCode)
    {
        return statusCode switch
        {
            StatusCodes.Status400BadRequest => "Validation error",
            StatusCodes.Status401Unauthorized => "Unauthorized",
            StatusCodes.Status403Forbidden => "Forbidden",
            StatusCodes.Status404NotFound => "Resource not found",
            StatusCodes.Status409Conflict => "Conflict",

            _ => "Unexpected error"
        };
    }
}