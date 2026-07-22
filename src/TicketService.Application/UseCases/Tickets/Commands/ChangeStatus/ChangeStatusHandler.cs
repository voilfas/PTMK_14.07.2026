using TicketService.Application.Abstractions;
using TicketService.Application.Abstractions.Persistence.Commands;
using TicketService.Application.Common.ErrorsHandler;
using TicketService.Domain.Common;

namespace TicketService.Application.UseCases.Tickets.Commands.ChangeStatus;

public class ChangeStatusHandler
{
    private readonly ITicketRepository _ticketRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ChangeStatusHandler(ITicketRepository ticketRepository, IUnitOfWork unitOfWork)
    {
        _ticketRepository = ticketRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(
        ChangeStatusCommand command,
        CancellationToken cancellationToken)
    {
        var ticket = await _ticketRepository.GetByIdAsync(command.Id, cancellationToken);

        if (ticket == null)
            return Result.Failure(ErrorsTicket.NotFoundById);
        
        var ticketChangedResult = ticket.ChangeStatus(command.Status);
        
        if (ticketChangedResult.IsFailure)
            return Result.Failure(ticketChangedResult.Error);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Result.Success();
    }
}