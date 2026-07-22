using TicketService.Domain.Common;

namespace TicketService.Domain.Errors;

public static class ErrorsDepartment
{
    public static readonly Error EmptyName = new Error("Department.EmptyName", "Name of department can't be empty");
    public static readonly Error EmptyCode = new Error("Department.EmptyCode", "Code of department can't be empty");
    
    public static readonly Error IncorrectName = new Error("Department.IncorrectName", "Name should contain 2 - 30 symbols");
    public static readonly Error IncorrectCode = new Error("Department.IncorrectCode", "First name should contain 5 - 15 symbols");
}