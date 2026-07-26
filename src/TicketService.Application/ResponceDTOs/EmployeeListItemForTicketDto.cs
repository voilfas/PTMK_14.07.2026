namespace TicketService.Application.ResponceDTOs;

public record EmployeeListItemForTicketDto(
    Guid EmployeeId,
    string FullName,
    string DepartmentName,
    string PositionName);