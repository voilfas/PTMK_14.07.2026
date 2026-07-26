using MediatR;
using TicketService.Application.Common;
using TicketService.Application.ResponceDTOs;

namespace TicketService.Application.UseCases.Positions.Queries.GetPositions;

public record GetPositionsQuery() : PageQuery, IRequest<PageResult<PositionDto>>;