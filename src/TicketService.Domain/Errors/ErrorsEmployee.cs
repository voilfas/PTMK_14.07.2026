using TicketService.Domain.Common;
using TicketService.Domain.Common.ErrorHandler;

namespace TicketService.Domain.Errors;

public static class ErrorsEmployee
{
    public static readonly Error EmptyFirstName = new Error("Employee.EmptyFirstName", "First name can't be empty");
    public static readonly Error EmptyLastName = new Error("Employee.EmptyLastName", "Last name can't be empty");
    public static readonly Error EmptySurname = new Error("Employee.EmptySurname", "Surname can't be empty");
    
    public static readonly Error IncorrectFirstName = new Error("Employee.IncorrectFirstName", "First name should contain 2 - 20 symbols");
    public static readonly Error IncorrectLasName = new Error("Employee.IncorrectLasName", "Last name should contain 2 - 20 symbols");
    public static readonly Error IncorrectSurname = new Error("Employee.IncorrectSurname", "Surname should contain 2 - 20 symbols");
}