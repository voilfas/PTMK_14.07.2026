using TicketService.Domain.Common;
using TicketService.Domain.Errors;

namespace TicketService.Domain.ValueObjects;

public record Department
{
    //private bool IsActive { get; } можно сделать сущностью
    private string Name { get; }
    private string Code { get;  }

    private Department(string name, string code)
    {
        Name = name;
        Code = code;
    }

    public static Result<Department> Create(string name, string code)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result<Department>.Failure(ErrorsDepartment.EmptyName);

        if (string.IsNullOrWhiteSpace(code))
            return Result<Department>.Failure(ErrorsDepartment.EmptyCode);

        if (name.Length is < 2 or > 30)
            return Result<Department>.Failure(ErrorsDepartment.IncorrectName);

        if (code.Length is < 5 or > 15)
            return Result<Department>.Failure(ErrorsDepartment.IncorrectCode);

        return Result<Department>.Success(new Department(name, code));
    }

    public override string ToString() => $"{Name} {Code}";
}