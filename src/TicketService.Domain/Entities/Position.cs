using TicketService.Domain.Common;
using TicketService.Domain.Errors;

namespace TicketService.Domain.Entities;

public class Position : BaseEntity
{
    public string Name { get; private set; }
    public bool IsActive { get; private set; }
    
    private Position(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
        IsActive = true;
    }

    public static Result<Position> Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result<Position>.Failure(ErrorsPosition.EmptyName);

        if (name.Length is < 2 or > 30)
            return Result<Position>.Failure(ErrorsPosition.InvalidName);

        return Result<Position>.Success(new Position(name));
    }
    
    public void Active() =>  IsActive = true;
    
    public void Deactivate() =>  IsActive = false;
}