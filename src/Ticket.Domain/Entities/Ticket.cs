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
    public Employee AuthorId { get; private set; }
    public Employee ExecutorId { get; private set; }
    public string Description { get; private set; }
    public DateTime DeadLine { get; private set; }
    public TicketStatus Status { get; private set; }

    private Ticket(Employee authorId, Employee executorId, string description, DateTime deadLine)
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

    public static Result<Ticket> Create(Employee? authorId, Employee? executorId, string? description, DateTime deadLine)
    {
        if (authorId is null)
            return Result<Ticket>.Failure(ErrorsTicket.EmptyAuthorId);
        
        if (executorId is null)
            return Result<Ticket>.Failure(ErrorsTicket.EmptyExecutorId);
        
        if (string.IsNullOrWhiteSpace(description))
            return Result<Ticket>.Failure(ErrorsTicket.EmptyDescription);

        if (deadLine <= DateTime.UtcNow || deadLine > DateTime.UtcNow.AddYears(1))
            return Result<Ticket>.Failure(ErrorsTicket.IncorrectDeadLine);

        return Result<Ticket>.Success(new Ticket(authorId, executorId, description, deadLine));
    }
}