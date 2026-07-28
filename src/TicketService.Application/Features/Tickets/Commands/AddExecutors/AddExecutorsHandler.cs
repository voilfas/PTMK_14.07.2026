using MediatR;
using TicketService.Application.Abstractions;
using TicketService.Application.Abstractions.Persistence.Commands;
using TicketService.Application.Common.Errors;
using TicketService.Domain.Common;

namespace TicketService.Application.Features.Tickets.Commands.AddExecutors;

public class AddExecutorsHandler : IRequestHandler<AddExecutorsCommand, Result>
{
    private readonly ITicketRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    
    public AddExecutorsHandler(ITicketRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(
        AddExecutorsCommand request,
        CancellationToken cancellationToken)
    {
        var ticket = await _repository.GetByIdAsync(request.TicketId, cancellationToken);

        if (ticket is null)
            return Result.Failure(ErrorsTicket.NotFoundById);

        var resultAddExecutors = ticket.AddExecutors(request.ExecutorsIds);
        if (resultAddExecutors.IsFailure)
            return Result.Failure(resultAddExecutors.Error);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Result.Success();
    }
}