using MediatR;
using Microsoft.AspNetCore.Mvc;
using TicketService.API.Common;
using TicketService.Application.UseCases.Departments.Commands.CreateDepartment;
using TicketService.Application.UseCases.Departments.Queries.GetDepartments;

namespace TicketService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DepartmentsController : ControllerBase
{
    private readonly ISender _sender;

    public DepartmentsController(ISender sender)
    {
        _sender = sender;
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateDepartmentCommand command,
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
        [FromQuery] GetDepartmentsQuery query,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(query, cancellationToken);

        return Ok(result);
    }
}