using MediatR;
using TicketService.Application.DTOs;
using TicketService.Domain.Common;

namespace TicketService.Application.UseCases.Tickets.Queries.GetTicketByIdForUpdate;

public record GetTicketForUpdateQuery(Guid Id) : IRequest<Result<TicketDto>>;