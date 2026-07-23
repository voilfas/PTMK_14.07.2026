using TicketService.Domain.Abstractions;
using TicketService.Domain.Common;
using TicketService.Domain.Enums;
using TicketService.Domain.Errors;
using TicketService.Domain.Events;
using TicketService.Domain.ValueObjects;

namespace TicketService.Domain.Entities;

public class Ticket : AggregateRoot
{
    public TicketNumber TicketNumber { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public Guid AuthorId { get; private set; }
    public string Description { get; private set; }
    public DateTime Deadline { get; private set; }
    public TicketStatus Status { get; private set; }
    public TicketType Type { get; private set; }

    private readonly List<TicketExecutor> _executors = [];
    public IReadOnlyCollection<TicketExecutor> Executors => _executors.AsReadOnly();

    private Ticket() {}

    private Ticket(
        Guid authorId,
        IReadOnlyCollection<Guid> executorIds,
        string description,
        TicketType type)
    {
        Id = Guid.NewGuid();

        TicketNumber = TicketNumber.Generate(Id);

        CreatedAt = DateTime.UtcNow;

        AuthorId = authorId;

        Description = description;

        Type = type;

        Deadline = CalculateDeadline(type);

        Status = TicketStatus.New;

        foreach (var executorId in executorIds.Distinct())
        {
            _executors.Add(new TicketExecutor(Id, executorId));
        }
    }

    public static Result<Ticket> Create(
        Guid authorId,
        IReadOnlyCollection<Guid> executorIds,
        string description,
        TicketType type)
    {
        if (authorId == Guid.Empty)
            return Result<Ticket>.Failure(ErrorsTicket.EmptyAuthorId);

        if (string.IsNullOrWhiteSpace(description))
            return Result<Ticket>.Failure(ErrorsTicket.EmptyDescription);

        if (description.Length is < 3 or > 300)
            return Result<Ticket>.Failure(ErrorsTicket.IncorrectDescription);

        if (executorIds.Any(id => id == Guid.Empty))
            return Result<Ticket>.Failure(ErrorsTicket.EmptyExecutorId);

        return Result<Ticket>.Success(
            new Ticket(authorId, executorIds, description, type));
    }

    public Result AddExecutors(IReadOnlyCollection<Guid> executorIds)
    {
        if (executorIds.Count == 0)
            return Result.Success();

        var canManage = CanManageExecutors();

        if (canManage.IsFailure)
            return canManage;

        if (executorIds.Any(id => id == Guid.Empty))
            return Result.Failure(ErrorsTicket.EmptyExecutorId);

        var newExecutors = executorIds
            .Distinct()
            .Where(id => _executors.All(e => e.EmployeeId != id))
            .ToList();

        foreach (var executorId in newExecutors)
        {
            _executors.Add(new TicketExecutor(Id, executorId));
        }

        if (newExecutors.Count > 0)
        {
            AddEvent(new TicketExecutorAddListEvent(Id, newExecutors));
        }

        return Result.Success();
    }

    public Result ChangeExecutor(
        Guid oldExecutorId,
        Guid newExecutorId)
    {
        if (oldExecutorId == Guid.Empty || newExecutorId == Guid.Empty)
            return Result.Failure(ErrorsTicket.EmptyExecutorId);

        if (oldExecutorId == newExecutorId)
            return Result.Success();

        var canManage = CanManageExecutors();

        if (canManage.IsFailure)
            return canManage;

        var oldExecutor = _executors
            .FirstOrDefault(e => e.EmployeeId == oldExecutorId);

        if (oldExecutor is null)
            return Result.Failure(ErrorsTicket.ExecutorDoesNotExist);

        if (_executors.Any(e => e.EmployeeId == newExecutorId))
            return Result.Failure(ErrorsTicket.ExecutorAlreadyExist);

        _executors.Remove(oldExecutor);

        _executors.Add(new TicketExecutor(Id, newExecutorId));

        AddEvent(new TicketExecutorChangedEvent(
            Id,
            oldExecutorId,
            newExecutorId));

        return Result.Success();
    }

    public Result DeleteExecutor(Guid executorId)
    {
        if (executorId == Guid.Empty)
            return Result.Failure(ErrorsTicket.EmptyExecutorId);

        var canManage = CanManageExecutors();

        if (canManage.IsFailure)
            return canManage;

        var executor = _executors
            .FirstOrDefault(e => e.EmployeeId == executorId);

        if (executor is null)
            return Result.Failure(ErrorsTicket.ExecutorDoesNotExist);

        _executors.Remove(executor);

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

        if (Status is TicketStatus.Completed or TicketStatus.Rejected)
            return Result.Failure(ErrorsTicket.CompletedOrRejectedTicketStatus);

        Type = newType;

        Deadline = CalculateDeadline(newType);

        return Result.Success();
    }

    private Result CanManageExecutors()
    {
        if (Status is not TicketStatus.New and not TicketStatus.InProgress)
            return Result.Failure(ErrorsTicket.CannotChangeExecutorsInCurrentStatus);

        return Result.Success();
    }

    private static DateTime CalculateDeadline(TicketType type)
    {
        var now = DateTime.UtcNow;

        return type switch
        {
            TicketType.Standard => now.AddDays(3),
            TicketType.Urgent => now.AddDays(1),
            TicketType.Critical => now.AddHours(4),
            _ => now.AddDays(3)
        };
    }

    private static readonly HashSet<(TicketStatus From, TicketStatus To)> AllowedTransitions =
    [
        (TicketStatus.New, TicketStatus.AwaitingApproval),
        (TicketStatus.AwaitingApproval, TicketStatus.Approved),
        (TicketStatus.AwaitingApproval, TicketStatus.Rejected),
        (TicketStatus.Rejected, TicketStatus.New),
        (TicketStatus.Approved, TicketStatus.InProgress),
        (TicketStatus.InProgress, TicketStatus.Completed)
    ];
}