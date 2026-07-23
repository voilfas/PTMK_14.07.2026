using MediatR;
using TicketService.Application.DTOs;
using TicketService.Domain.Common;

namespace TicketService.Application.UseCases.Tickets.Queries.GetTicketByIdRead;

public record GetTicketByIdReadQuery(
    Guid Id) : IRequest<Result<TicketDto>>;