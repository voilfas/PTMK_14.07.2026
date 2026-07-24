using MediatR;
using Microsoft.AspNetCore.Mvc;
using TicketService.API.Common;
using TicketService.Application.UseCases.Employees.Commands.ChangeExecutor;
using TicketService.Application.UseCases.Tickets.Commands.AddExecutors;
using TicketService.Application.UseCases.Tickets.Commands.ChangeStatus;
using TicketService.Application.UseCases.Tickets.Commands.CreateTicket;
using TicketService.Application.UseCases.Tickets.Commands.DeleteExecutor;
using TicketService.Application.UseCases.Tickets.Queries.GetOverdueTickets;
using TicketService.Application.UseCases.Tickets.Queries.GetTicketByIdRead;
using TicketService.Application.UseCases.Tickets.Queries.GetTickets;

namespace TicketService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TicketsController : ControllerBase
{
    private readonly ISender _sender;

    public TicketsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateTicketCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
            return this.Problem(result.Error);

        return CreatedAtAction(
            nameof(GetById),
            new { id = result.Value },
            result.Value);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(
        Guid id,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new GetTicketByIdReadQuery(id),
            cancellationToken);

        if (result.IsFailure)
            return this.Problem(result.Error);

        return Ok(result.Value);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] TicketFilter filter,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            new GetTicketsQuery(filter),
            cancellationToken);

        return Ok(result);
    }

    [HttpGet("overdue")]
    public async Task<IActionResult> GetOverdue(
        [FromQuery] GetOverdueTicketsQuery query,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(query, cancellationToken);

        return Ok(result);
    }

    [HttpPatch("{id:guid}/status")]
    public async Task<IActionResult> ChangeStatus(
        Guid id,
        [FromBody] ChangeStatusCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            command with { Id = id },
            cancellationToken);

        if (result.IsFailure)
            return this.Problem(result.Error);

        return NoContent();
    }

    [HttpPost("{id:guid}/executors")]
    public async Task<IActionResult> AddExecutors(
        Guid id,
        [FromBody] AddExecutorsCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            command with { TicketId = id },
            cancellationToken);

        if (result.IsFailure)
            return this.Problem(result.Error);

        return NoContent();
    }

    [HttpPut("{id:guid}/executors")]
    public async Task<IActionResult> ChangeExecutor(
        Guid id,
        [FromBody] ChangeExecutorCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(
            command with { TicketId = id },
            cancellationToken);

        if (result.IsFailure)
            return this.Problem(result.Error);

        return NoContent();
    }

    [HttpDelete("{id:guid}/executors/{executorId:guid}")]
    public async Task<IActionResult> DeleteExecutor(
        Guid id,
        Guid executorId,
        CancellationToken cancellationToken)
    {
        var command = new DeleteExecutorCommand(id, executorId);

        var result = await _sender.Send(command, cancellationToken);

        if (result.IsFailure)
            return this.Problem(result.Error);

        return NoContent();
    }
}