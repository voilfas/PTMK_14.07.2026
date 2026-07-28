using MediatR;
using TicketService.Application.Abstractions;
using TicketService.Application.Abstractions.Persistence.Commands;
using TicketService.Application.Common.Errors;
using TicketService.Domain.Common;

namespace TicketService.Application.Features.Tickets.Commands.DeleteExecutor;

public class DeleteExecutorHandler : IRequestHandler<DeleteExecutorCommand, Result>
{
    private readonly ITicketRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteExecutorHandler(ITicketRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(
        DeleteExecutorCommand request,
        CancellationToken cancellationToken)
    {
        var ticket = await _repository.GetByIdAsync(request.TicketId, cancellationToken);
        if (ticket is null)
            return Result.Failure(ErrorsTicket.NotFoundById);

        var ticketDeleteExecutor = ticket.DeleteExecutor(request.ExecutorId);
        if (ticketDeleteExecutor.IsFailure)
            return Result.Failure(ticketDeleteExecutor.Error);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}