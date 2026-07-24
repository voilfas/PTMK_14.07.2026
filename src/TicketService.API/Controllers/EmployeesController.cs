using MediatR;
using Microsoft.AspNetCore.Mvc;
using TicketService.API.Common;
using TicketService.Application.UseCases.Employees.Commands.CreateEmployee;
using TicketService.Application.UseCases.Employees.Queries.GetEmployees;

namespace TicketService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly ISender _sender;

    public EmployeesController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateEmployeeCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
            return this.Problem(result.Error);

        return CreatedAtAction(
            nameof(GetAll),
            new { id = result.Value },
            result.Value);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] EmployeeFilter filter,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new GetEmployeesQuery(filter),
            cancellationToken);

        return Ok(result);
    }
}