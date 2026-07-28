using MediatR;
using TicketService.Application.DTOs.TicketDTOs;

namespace TicketService.Application.Features.Tickets.Queries.GetAmountByStatus;

public sealed record GetAmountByStatusQuery() : IRequest<IReadOnlyCollection<TicketStatusReportDto>>;