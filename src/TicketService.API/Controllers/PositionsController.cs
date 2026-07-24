using MediatR;
using Microsoft.AspNetCore.Mvc;
using TicketService.API.Common;
using TicketService.Application.UseCases.Positions.Commands.CreatePosition;
using TicketService.Application.UseCases.Positions.Queries;

namespace TicketService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PositionsController : ControllerBase
{
    private readonly ISender _sender;

    public PositionsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreatePositionCommand command,
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
        [FromQuery] GetPositionsQuery query,
        CancellationToken cancellationToken)
    {
        var result = await _sender.Send(query, cancellationToken);

        return Ok(result);
    }
}