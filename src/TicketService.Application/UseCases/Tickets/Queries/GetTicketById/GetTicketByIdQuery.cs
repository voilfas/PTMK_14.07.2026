using MediatR;
using TicketService.Application.ResponceDTOs;
using TicketService.Domain.Common;

namespace TicketService.Application.UseCases.Tickets.Queries.GetTicketById;

public record GetTicketByIdQuery(
    Guid Id) : IRequest<Result<TicketDto>>;