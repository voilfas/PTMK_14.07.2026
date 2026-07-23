namespace TicketService.Domain.ValueObjects;

public record TicketNumber
{
    public string Number { get; }

    private TicketNumber(string number)
    {
        Number = number;
    }

    public static TicketNumber Generate(Guid id)
    {
        var guid = id.ToString("N");
        
        var number = $"TICKET-{guid[..26]}";

        return new TicketNumber(number);
    }
    
    // Метод используется EF Core для восстановления из БД
    public static TicketNumber FromDatabase(string number)
    {
        return new TicketNumber(number);
    }
    
    public override string ToString() => Number;
}