using TicketService.Domain.Common.ErrorHandler;
using TicketService.Domain.Errors;

namespace TicketService.Domain.Entities;

public class Department : BaseEntity
{
    public string Name { get; private set; }
    public string Code { get; private set; }
    public bool IsActive { get; private set; }

    private Department(string name, string code)
    {
        Id =  Guid.NewGuid();
        Name = name;
        Code = code;
        IsActive = true;
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

    public Result ChangeName(string newName)
    {
        if (Name ==  newName)
            return Result.Success();
        
        if (string.IsNullOrWhiteSpace(newName)) 
            return Result.Failure(ErrorsDepartment.EmptyName);
        
        if (newName.Length is < 2 or > 30)
            return Result.Failure(ErrorsDepartment.IncorrectName);
        
        Name =  newName;
        return Result.Success();
    }

    public void Activate() => IsActive = true;

    public void Deactivate() => IsActive = false;
}