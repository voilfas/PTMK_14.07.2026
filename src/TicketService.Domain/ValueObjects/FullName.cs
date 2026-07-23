using TicketService.Domain.Common;
using TicketService.Domain.Errors;

namespace TicketService.Domain.ValueObjects;

public record FullName
{
    public string FirstName { get; }
    public string LastName { get; }
    public string Surname { get; }

    private FullName(string firstName, string lastName, string surname)
    {
        FirstName = firstName;
        LastName = lastName;
        Surname = surname;
    }

    public static Result<FullName> Create(string firstName, string lastName, string surname)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            return Result<FullName>.Failure(ErrorsEmployee.EmptyFirstName);

        if (string.IsNullOrWhiteSpace(lastName))
            return Result<FullName>.Failure(ErrorsEmployee.EmptyLastName);
        
        if (string.IsNullOrWhiteSpace(surname))
            return Result<FullName>.Failure(ErrorsEmployee.EmptySurname);

        if (firstName.Length is < 2 or > 20)
            return Result<FullName>.Failure(ErrorsEmployee.IncorrectFirstName);
        
        if (lastName.Length is < 2 or > 20)
            return Result<FullName>.Failure(ErrorsEmployee.IncorrectLasName);

        if (surname.Length is < 2 or > 20)
            return Result<FullName>.Failure(ErrorsEmployee.IncorrectSurname);

        return Result<FullName>.Success(new FullName(firstName, lastName, surname));
    }
    
    public override string ToString() => $"{FirstName} {LastName} {Surname}";
}