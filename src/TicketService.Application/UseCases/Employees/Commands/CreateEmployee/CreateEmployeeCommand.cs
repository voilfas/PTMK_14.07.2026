namespace TicketService.Application.UseCases.Employees.Commands.CreateEmployee;

public record CreateEmployeeCommand(
    string FirstName,
    string LastName,
    string Surname,
    Guid DepartmentId,
    Guid PositionId
    );