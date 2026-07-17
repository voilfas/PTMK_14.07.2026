using TicketService.Domain.Abstractions;
using TicketService.Domain.Common;
using TicketService.Domain.Enums;
using TicketService.Domain.Errors;
using TicketService.Domain.Events;
using TicketService.Domain.ValueObjects;

namespace TicketService.Domain.Entities;

public class Ticket : AggregateRoot
{
    public Guid Id { get; private set; }
    public TicketNumber TicketNumber { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public Guid AuthorId { get; private set; }
    public string Description { get; private set; }
    public DateTime Deadline { get; private set; }
    public TicketStatus Status { get; private set; }
    public TicketType Type { get; private set; }
    
    private readonly List<Guid> _executorIds = [];
    public IReadOnlyList<Guid> ExecutorIds => _executorIds.AsReadOnly();

    private Ticket(Guid authorId, List<Guid>? executorIds, string description, TicketType type)
    {
        Id = Guid.NewGuid();
        TicketNumber = TicketNumber.Generate(Id);
        CreatedAt = DateTime.UtcNow;
        AuthorId = authorId;
        if (executorIds != null) _executorIds.AddRange(executorIds);
        Description = description;
        Deadline = CalculateDeadline(type);
        Status = TicketStatus.New;
    }

    public static Result<Ticket> Create(Guid authorId, List<Guid>? executorIds, string description, TicketType type)
    {
        if (authorId == Guid.Empty)
            return Result<Ticket>.Failure(ErrorsTicket.EmptyAuthorId);

        if (executorIds is not null)
        {
            var distinctExecutorIds = executorIds.Distinct().ToList();
            if (distinctExecutorIds.Count != executorIds.Count)
                return Result<Ticket>.Failure(ErrorsTicket.DistinctExecutors);
            
            if (distinctExecutorIds.Any(id => id == Guid.Empty))
                return Result<Ticket>.Failure(ErrorsTicket.EmptyExecutorId);
        }
        
        if (string.IsNullOrWhiteSpace(description))
            return Result<Ticket>.Failure(ErrorsTicket.EmptyDescription);
        
        if (description.Length is < 3 or > 300)
            return Result<Ticket>.Failure(ErrorsTicket.IncorrectDescription);

        return Result<Ticket>.Success(new Ticket(authorId, executorIds, description, type));
    }

    public Result AddExecutors(List<Guid>? executorIds)
    {
        if (executorIds == null || !executorIds.Any())
            return Result.Success();
        
        if (Status == TicketStatus.Completed || Status == TicketStatus.Rejected)
            return Result.Failure(ErrorsTicket.CompletedOrRejectedTicketStatus);

        if (Status != TicketStatus.New && Status != TicketStatus.InProgress)
            return Result.Failure(ErrorsTicket.CannotChangeExecutorsInCurrentStatus);
        
        var distinctExecutorIds = executorIds.Distinct().ToList();

        if (distinctExecutorIds.Count != executorIds.Count)
            return Result.Failure(ErrorsTicket.DistinctExecutors);

        if (distinctExecutorIds.Any(executorId => executorId == Guid.Empty))
            return Result.Failure(ErrorsTicket.EmptyExecutorId);

        var newIds = distinctExecutorIds.Where(guid => !_executorIds.Contains(guid)).ToList();
        if(!newIds.Any())
            return Result.Success();
        
        foreach (var newExecutorId in newIds)
        {
            _executorIds.Add(newExecutorId);
        }
        
        AddEvent(new TicketExecutorAddListEvent(Id, newIds));
        
        return Result.Success();
    }

    public Result ChangeExecutor(Guid oldExecutorId, Guid newExecutorId)
    {
        if (oldExecutorId == Guid.Empty ||  newExecutorId == Guid.Empty)
            return Result.Failure(ErrorsTicket.EmptyExecutorId);
        
        if (oldExecutorId == newExecutorId)
            return Result.Success();
        
        if (Status != TicketStatus.New && Status != TicketStatus.InProgress)
            return Result.Failure(ErrorsTicket.CannotChangeExecutorsInCurrentStatus);
        
        if (Status == TicketStatus.Completed || Status == TicketStatus.Rejected)
            return Result.Failure(ErrorsTicket.CompletedOrRejectedTicketStatus);
        
        if (!_executorIds.Contains(oldExecutorId))
            return Result.Failure(ErrorsTicket.ExecutorDoesNotExist);
        
        if (_executorIds.Contains(newExecutorId))
            return Result.Failure(ErrorsTicket.ExecutorAlreadyExist);
        
        _executorIds.Remove(oldExecutorId);
        _executorIds.Add(newExecutorId);
        
        AddEvent(new TicketExecutorChangedEvent(Id, oldExecutorId, newExecutorId));
        
        return Result.Success();
    }

    public Result DeleteExecutor(Guid executorId)
    {
        if (executorId == Guid.Empty)
            return Result.Failure(ErrorsTicket.EmptyExecutorId);
        
        if (!_executorIds.Contains(executorId))
            return Result.Failure(ErrorsTicket.ExecutorDoesNotExist);
        
        _executorIds.Remove(executorId);
        
        AddEvent(new TicketExecutorRemoveEvent(Id, executorId));
        
        return Result.Success();
    }
    
    public Result ChangeStatus(TicketStatus newStatus)
    {
        if (Status == newStatus)
            return Result.Success();

        if (!AllowedTransitions.Contains((Status, newStatus)))
            return Result.Failure(ErrorsTicket.InvalidStatusTransition);
        
        var oldStatus = Status;
        Status = newStatus;
        AddEvent(new TicketStatusChangedEvent(Id, oldStatus, newStatus));
        
        return Result.Success();
    }

    public Result ChangeType(TicketType newType)
    {
        if (Type == newType)
            return Result.Success();
        
        if (Status == TicketStatus.Completed || Status == TicketStatus.Rejected)
            return Result.Failure(ErrorsTicket.CompletedOrRejectedTicketStatus);
        
        Type = newType;
        Deadline = CalculateDeadline(newType);
        
        return Result.Success();
    }
    
    private DateTime CalculateDeadline(TicketType type)
    {
        var now = DateTime.UtcNow;

        return type switch
        {
            TicketType.Standart => now.AddDays(3),
            TicketType.Urgent => now.AddDays(1),
            TicketType.Critical => now.AddHours(4),
            _ => now.AddDays(3)
        };
    }
    
    private static readonly HashSet<(TicketStatus From, TicketStatus To)> AllowedTransitions = new()
    {
        (TicketStatus.New, TicketStatus.AwaitingApproval),
        (TicketStatus.AwaitingApproval, TicketStatus.Approved),
        (TicketStatus.AwaitingApproval, TicketStatus.Rejected),
        (TicketStatus.Rejected, TicketStatus.New),
        (TicketStatus.Approved, TicketStatus.InProgress),
        (TicketStatus.InProgress, TicketStatus.Completed),
    };
}