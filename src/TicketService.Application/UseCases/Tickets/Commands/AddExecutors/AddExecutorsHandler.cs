using TicketService.Application.Abstractions;
using TicketService.Application.Abstractions.Persistence.Commands;
using TicketService.Application.Common.ErrorsHandler;
using TicketService.Domain.Common;

namespace TicketService.Application.UseCases.Tickets.Commands.AddExecutors;

public class AddExecutorsHandler
{
    private readonly ITicketRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    
    public AddExecutorsHandler(ITicketRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(
        AddExecutorsCommand command,
        CancellationToken cancellationToken)
    {
        var ticket = await _repository.GetByIdAsync(command.TicketId, cancellationToken);

        if (ticket is null)
            return Result.Failure(ErrorsTicket.NotFoundById);

        var resultAddExecutors = ticket.AddExecutors(command.ExecutorsIds);
        if (resultAddExecutors.IsFailure)
            return Result.Failure(resultAddExecutors.Error);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Result.Success();
    }
}