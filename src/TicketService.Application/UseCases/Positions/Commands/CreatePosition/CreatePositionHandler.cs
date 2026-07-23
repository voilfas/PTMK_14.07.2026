using MediatR;
using TicketService.Application.Abstractions;
using TicketService.Application.Abstractions.Persistence.Commands;
using TicketService.Domain.Common;
using TicketService.Domain.Entities;

namespace TicketService.Application.UseCases.Positions.Commands.CreatePosition;

public class CreatePositionHandler : IRequestHandler<CreatePositionCommand, Result<Guid>>
{
    private readonly IPositionRepository _repository;
    private  readonly IUnitOfWork _unitOfWork;

    public CreatePositionHandler(IPositionRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(
        CreatePositionCommand command,
        CancellationToken cancellationToken)
    {
        var positionResult = Position.Create(command.Name);
        if (positionResult.IsFailure)
            return Result<Guid>.Failure(positionResult.Error);
        
        var position = positionResult.Value;
        
        await _repository.AddAsync(position, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result<Guid>.Success(position.Id);
    }
}