using TicketService.Domain.Common;

namespace TicketService.API.Common;

public static class ErrorMapper
{
    public static int GetStatusCode(Error error)
    {
        var code = error.Code;

        // ---------- 404 ----------
        if (code.EndsWith("NotFoundById", StringComparison.OrdinalIgnoreCase))
            return StatusCodes.Status404NotFound;

        // ---------- 409 ----------
        if (code.EndsWith("AlreadyExists", StringComparison.OrdinalIgnoreCase) ||
            code.EndsWith("AlreadyExist", StringComparison.OrdinalIgnoreCase))
            return StatusCodes.Status409Conflict;

        // ---------- 401 ----------
        if (code.Contains("Unauthorized", StringComparison.OrdinalIgnoreCase))
            return StatusCodes.Status401Unauthorized;

        // ---------- 403 ----------
        if (code.Contains("Forbidden", StringComparison.OrdinalIgnoreCase))
            return StatusCodes.Status403Forbidden;

        // ---------- 400 ----------
        return StatusCodes.Status400BadRequest;
    }
}