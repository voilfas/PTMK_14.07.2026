using MediatR;
using TicketService.Domain.Common;

namespace TicketService.Application.UseCases.Positions.Commands.CreatePosition;

public record CreatePositionCommand(string Name) :  IRequest<Result<Guid>>;