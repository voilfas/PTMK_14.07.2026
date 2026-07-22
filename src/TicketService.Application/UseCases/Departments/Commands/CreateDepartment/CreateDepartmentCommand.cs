namespace TicketService.Application.UseCases.Departments.Commands.CreateDepartment;

public record CreateDepartmentCommand(
    string Name,
    string Code);