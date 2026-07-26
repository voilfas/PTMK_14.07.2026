using MediatR;
using TicketService.Application.DTOs;
using TicketService.Domain.Common;

namespace TicketService.Application.UseCases.Tickets.Queries.GetAmountByStatus;

public sealed record GetAmountByStatusRequest() : IRequest<IReadOnlyCollection<TicketStatusReportDto>>;