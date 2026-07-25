using MediatR;
using TicketService.Application.Common;
using TicketService.Application.DTOs;

namespace TicketService.Application.UseCases.Positions.Queries.GetPositions;

public record GetPositionsQuery() : PageQuery, IRequest<PageResult<PositionDto>>;