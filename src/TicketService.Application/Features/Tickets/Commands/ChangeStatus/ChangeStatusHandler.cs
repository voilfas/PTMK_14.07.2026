using MediatR;
using TicketService.Application.Abstractions;
using TicketService.Application.Abstractions.Persistence.Commands;
using TicketService.Application.Common.Errors;
using TicketService.Domain.Common;

namespace TicketService.Application.Features.Tickets.Commands.ChangeStatus;

public class ChangeStatusHandler : IRequestHandler<ChangeStatusCommand, Result>
{
    private readonly ITicketRepository _ticketRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ChangeStatusHandler(ITicketRepository ticketRepository, IUnitOfWork unitOfWork)
    {
        _ticketRepository = ticketRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(
        ChangeStatusCommand request,
        CancellationToken cancellationToken)
    {
        var ticket = await _ticketRepository.GetByIdAsync(request.Id, cancellationToken);

        if (ticket == null)
            return Result.Failure(ErrorsTicket.NotFoundById);
        
        var ticketChangedResult = ticket.ChangeStatus(request.Status);
        
        if (ticketChangedResult.IsFailure)
            return Result.Failure(ticketChangedResult.Error);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Result.Success();
    }
}