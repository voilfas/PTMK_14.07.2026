using MediatR;
using TicketService.Application.DTOs.TicketDTOs;
using TicketService.Domain.Common;

namespace TicketService.Application.Features.Tickets.Queries.GetTicketById;

public record GetTicketByIdQuery(
    Guid Id) : IRequest<Result<TicketDto>>;