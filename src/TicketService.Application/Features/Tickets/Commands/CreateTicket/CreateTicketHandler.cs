using MediatR;
using TicketService.Application.Abstractions;
using TicketService.Application.Abstractions.Persistence.Commands;
using TicketService.Application.Common.Errors;
using TicketService.Domain.Common;
using TicketService.Domain.Entities;

namespace TicketService.Application.Features.Tickets.Commands.CreateTicket;

public class CreateTicketHandler : IRequestHandler<CreateTicketCommand, Result<Guid>>
{
    private readonly ITicketRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEmployeeRepository _employeeRepository;

    public CreateTicketHandler(
        ITicketRepository repository,
        IUnitOfWork unitOfWork,
        IEmployeeRepository employeeRepository)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _employeeRepository = employeeRepository;
    }

    public async Task<Result<Guid>> Handle(
        CreateTicketCommand request,
        CancellationToken cancellationToken)
    {
        if (!await _employeeRepository.ExistsAsync(request.AuthorId, cancellationToken))
            return Result<Guid>.Failure(ErrorsEmployee.NotFoundById);
            
        if (!await _employeeRepository.AllExistsAsync(request.ExecutorIds, cancellationToken))
            return Result<Guid>.Failure(ErrorsEmployee.NotFoundById);
        
        var ticketResult = Ticket.Create(
            request.AuthorId,
            request.ExecutorIds,
            request.Description,
            request.TicketType);
        
        if (ticketResult.IsFailure)
            return Result<Guid>.Failure(ticketResult.Error);
        
        var ticket = ticketResult.Value!;
        
        await _repository.AddAsync(ticket, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<Guid>.Success(ticket.Id);
    }
}