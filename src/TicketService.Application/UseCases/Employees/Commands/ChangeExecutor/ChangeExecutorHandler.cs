using TicketService.Application.Abstractions;
using TicketService.Application.Abstractions.Persistence.Commands;
using TicketService.Application.Common.ErrorsHandler;
using TicketService.Domain.Common;

namespace TicketService.Application.UseCases.Employees.Commands.ChangeExecutor;

public class ChangeExecutorHandler
{
    private readonly ITicketRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public ChangeExecutorHandler(ITicketRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(
        ChangeExecutorCommand command,
        CancellationToken cancellationToken)
    {
        var ticket = await _repository.GetByIdAsync(command.TicketId, cancellationToken);
        if (ticket is null)
            return Result.Failure(ErrorsTicket.NotFoundById);

        var changeExecutorResult = ticket.ChangeExecutor(command.OldExecutorId, command.NewExecutorId);
        if (changeExecutorResult.IsFailure)
            return Result.Failure(changeExecutorResult.Error);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}