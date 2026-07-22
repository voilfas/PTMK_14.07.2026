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
    
    private readonly HashSet<Guid> _executorIds = [];
    public IReadOnlyCollection<Guid> ExecutorIds => _executorIds;

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
        Status = TicketStatus.New;
        Type = type;
        Deadline = CalculateDeadline(type);
        _executorIds.UnionWith(executorIds);
    }

    public static Result<Ticket> Create(
        Guid authorId,
        IReadOnlyCollection<Guid> executorIds,
        string description,
        TicketType type)
    {
        // Пустой Guid автора
        if (authorId == Guid.Empty)
            return Result<Ticket>.Failure(ErrorsTicket.EmptyAuthorId);

        // Пустой Guid у исполнителей
        if (executorIds.Any(id => id == Guid.Empty))
            return Result<Ticket>.Failure(ErrorsTicket.EmptyExecutorId);

        // Повторяющиеся Guid у исполнителей
        if (executorIds.Distinct().Count() != executorIds.Count)
            return Result<Ticket>.Failure(ErrorsTicket.DistinctExecutors);

        // Пустое описание 
        if (string.IsNullOrWhiteSpace(description))
            return Result<Ticket>.Failure(ErrorsTicket.EmptyDescription);

        // Невалидная длина описания
        if (description.Length is < 3 or > 300)
            return Result<Ticket>.Failure(ErrorsTicket.IncorrectDescription);

        return Result<Ticket>.Success(
            new Ticket(authorId, executorIds, description, type));
    }

    public Result AddExecutors(IReadOnlyCollection<Guid> executorIds)
    {
        // Проверка на пустую коллекцию
        if (executorIds.Count == 0)
            return Result.Success();
        
        // Проверка на статус заявки
        var canManage = CanManageExecutors();
        if (canManage.IsFailure)
            return canManage;

        // Проверка на повторяющиеся элементы в коллекции
        var newExecutors = new HashSet<Guid>(executorIds);

        if (newExecutors.Count != executorIds.Count)
            return Result.Failure(ErrorsTicket.DistinctExecutors);
        
        // Проверка на Пустые Guid в коллекции
        if (newExecutors.Any(id => id == Guid.Empty))
            return Result.Failure(ErrorsTicket.EmptyExecutorId);
        
        // Проверка сколько уникальных Guid добавили в коллекцию
        var addedExecutors = new List<Guid>();

        foreach (var id in newExecutors)
        {
            if(_executorIds.Add(id))
                addedExecutors.Add(id);
        }
        
        if (addedExecutors.Count == 0)
            return Result.Success();
        
        AddEvent(new TicketExecutorAddListEvent(Id, addedExecutors));
        
        return Result.Success();
    }

    public Result ChangeExecutor(
        Guid oldExecutorId,
        Guid newExecutorId)
    {
        // Индемпотентность Guid-ов
        if (oldExecutorId == newExecutorId)
            return Result.Success();
        
        // Пустой Guid
        if (oldExecutorId == Guid.Empty ||  newExecutorId == Guid.Empty)
            return Result.Failure(ErrorsTicket.EmptyExecutorId);
        
        // Статус заявки
        var canManage = CanManageExecutors();
        if (canManage.IsFailure)
            return canManage;
        
        // Старого Guid не найдено в коллекции
        if (!_executorIds.Contains(oldExecutorId))
            return Result.Failure(ErrorsTicket.ExecutorDoesNotExist);
        
        // Новый Guid уже содержится в коллекции
        if (_executorIds.Contains(newExecutorId))
            return Result.Failure(ErrorsTicket.ExecutorAlreadyExist);
        
        _executorIds.Remove(oldExecutorId);
        _executorIds.Add(newExecutorId);
        
        AddEvent(new TicketExecutorChangedEvent(Id, oldExecutorId, newExecutorId));
        
        return Result.Success();
    }

    public Result DeleteExecutor(Guid executorId)
    {
        // Пустой Guid
        if (executorId == Guid.Empty)
            return Result.Failure(ErrorsTicket.EmptyExecutorId);
        
        // Не найден Guid в коллекции
        if (!_executorIds.Contains(executorId))
            return Result.Failure(ErrorsTicket.ExecutorDoesNotExist);
        
        // Проверка на статус заявки
        var canManage = CanManageExecutors();
        if (canManage.IsFailure)
            return canManage;
        
        _executorIds.Remove(executorId);
        
        AddEvent(new TicketExecutorRemoveEvent(Id, executorId));
        
        return Result.Success();
    }
    
    public Result ChangeStatus(TicketStatus newStatus)
    {
        // Идемпотентность
        if (Status == newStatus)
            return Result.Success();

        // Допустимость смены статуса
        if (!AllowedTransitions.Contains((Status, newStatus)))
            return Result.Failure(ErrorsTicket.InvalidStatusTransition);
        
        var oldStatus = Status;
        Status = newStatus;
        AddEvent(new TicketStatusChangedEvent(Id, oldStatus, newStatus));
        
        return Result.Success();
    }

    public Result ChangeType(TicketType newType)
    {
        // Идемпотентность
        if (Type == newType)
            return Result.Success();
        
        // Проверка статуса
        if (Status is TicketStatus.Completed or TicketStatus.Rejected)
            return Result.Failure(ErrorsTicket.CompletedOrRejectedTicketStatus);
        
        Type = newType;
        Deadline = CalculateDeadline(newType);
        
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

    private Result CanManageExecutors()
    {
        if (Status is not TicketStatus.New and not TicketStatus.InProgress)
            return Result.Failure(ErrorsTicket.CannotChangeExecutorsInCurrentStatus);

        return Result.Success();
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