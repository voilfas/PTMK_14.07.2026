using TicketService.Domain.Common;
using TicketService.Domain.Common.ErrorHandler;
using TicketService.Domain.Errors;

namespace TicketService.Domain.ValueObjects;

public class Department
{
    public string Name { get; private set; }
    public string Code { get; private set; }
    public bool IsActive { get; private set; }

    private Department(string name, string code)
    {
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

    public Result ActivateDepartment()
    {
        if (IsActive)
            return Result.Success();
        
        IsActive = true;
        
        return Result.Success();
    }

    public Result DeactivateDepartment()
    {
        if (!IsActive)
            return Result.Success();
        
        IsActive = false;
        
        return Result.Success();
    }
}