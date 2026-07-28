using MediatR;
using TicketService.Application.Common.Pagination;
using TicketService.Application.DTOs.PositionDTOs;

namespace TicketService.Application.Features.Positions.Queries.GetPositions;

public record GetPositionsQuery() : PageQuery, IRequest<PageResult<PositionDto>>;