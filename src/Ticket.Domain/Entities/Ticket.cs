using Ticket.Domain.Common;
using Ticket.Domain.Enums;
using Ticket.Domain.Errors;
using Ticket.Domain.ValueObjects;

namespace Ticket.Domain.Entities;

public class Ticket
{
    public Guid Id { get; private set; }
    public TicketNumber TicketNumber { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public Guid AuthorId { get; private set; }
    public Guid? ExecutorId { get; private set; }
    public string Description { get; private set; }
    public DateTime DeadLine { get; private set; }
    public TicketStatus Status { get; private set; }

    private Ticket(Guid authorId, string description, DateTime deadLine, Guid? executorId = null)
    {
        Id = Guid.NewGuid();
        TicketNumber = TicketNumber.Generate(Id);
        CreatedAt = DateTime.UtcNow;
        AuthorId = authorId;
        ExecutorId = executorId;
        Description = description;
        DeadLine = deadLine;
        Status = TicketStatus.New;
    }

    public static Result<Ticket> Create(Guid authorId, Guid? executorId, string description, DateTime deadLine)
    {
        if (authorId == Guid.Empty)
            return Result<Ticket>.Failure(ErrorsTicket.EmptyAuthorId);

        if (executorId.HasValue && executorId.Value == Guid.Empty)
            return Result<Ticket>.Failure(ErrorsTicket.EmptyExecutorId);
        
        if (string.IsNullOrWhiteSpace(description))
            return Result<Ticket>.Failure(ErrorsTicket.EmptyDescription);
        
        if (description.Length is < 3 or > 300)
            return Result<Ticket>.Failure(ErrorsTicket.IncorrectDescription);

        if (deadLine <= DateTime.UtcNow || deadLine > DateTime.UtcNow.AddYears(1))
            return Result<Ticket>.Failure(ErrorsTicket.IncorrectDeadLine);

        return Result<Ticket>.Success(new Ticket(authorId, description, deadLine, executorId));
    }
    
    public Result ChangeStatus(TicketStatus status)
    {
        if (Status == status)
            return Result.Success();
        
        if (Status == TicketStatus.New && status == TicketStatus.Completed)
            return Result.Failure(ErrorsTicket.FailStatusFromNewToCompleted);

        if (Status == TicketStatus.Completed && status == TicketStatus.InProgress)
            return Result.Failure(ErrorsTicket.FailStatusFromCompletedToInProgress);

        if (!((Status == TicketStatus.InProgress && status == TicketStatus.Completed) ||
              (Status == TicketStatus.New && status == TicketStatus.InProgress)))
            return Result.Failure(ErrorsTicket.InvalidStatusTransition);
        
        Status = status;
        
        return Result.Success();
    }

    public Result ChangeExecutor(Guid executorId)
    {
        if (executorId == Guid.Empty)
            return Result.Failure(ErrorsTicket.EmptyExecutorId);
        
        if (Status == TicketStatus.Completed)
            return Result.Failure(ErrorsTicket.InvalidChangeExecutor);

        if (executorId == ExecutorId)
            return Result.Success();
        
        ExecutorId = executorId;
        
        return Result.Success();
    }
}