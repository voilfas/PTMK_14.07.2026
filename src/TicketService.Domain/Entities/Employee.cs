using TicketService.Domain.Common;
using TicketService.Domain.Common.ErrorHandler;
using TicketService.Domain.Enums;
using TicketService.Domain.ValueObjects;

namespace TicketService.Domain.Entities;

public class Employee :  BaseEntity
{
    //Фио Подразделение Должность
    public FullName FullName { get; private set; }
    public Guid DepartmentId { get; private set; }
    public Guid PositionId { get; private set; }

    private Employee(FullName name, Guid departmentId, Guid positionId)
    {
        Id = Guid.NewGuid();
        FullName = name;
        DepartmentId = departmentId;
        PositionId = positionId;
    }

    public static Result<Employee> Create(FullName name, Guid departmentId, Guid positionId)
    {
        return Result<Employee>.Success(new Employee(name, departmentId, positionId));
    }
}