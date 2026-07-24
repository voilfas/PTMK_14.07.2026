using Microsoft.AspNetCore.Mvc;
using TicketService.Domain.Common;

namespace TicketService.API.Common;

public static class ControllerExtensions
{
    public static IActionResult Problem(
        this ControllerBase controller,
        Error error)
    {
        var problem = ProblemDetailsFactory.Create(error);

        return new ObjectResult(problem)
        {
            StatusCode = problem.Status
        };
    }
}