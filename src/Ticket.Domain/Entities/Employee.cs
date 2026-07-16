using Ticket.Domain.Common;
using Ticket.Domain.Enums;
using Ticket.Domain.ValueObjects;

namespace Ticket.Domain.Entities;

public class Employee
{
    //Фио Подразделение Должность
    public Guid Id { get; private set; }
    public FullName FullName { get; private set; }
    public Department Department { get; private set; }
    public Position Position { get; private set; }

    private Employee(FullName name, Department department, Position position)
    {
        Id = Guid.NewGuid();
        FullName = name;
        Department = department;
        Position = position;
    }

    public static Result<Employee> Create(FullName name, Department department, Position position)
    {
        return Result<Employee>.Success(new Employee(name, department, position));
    }
}