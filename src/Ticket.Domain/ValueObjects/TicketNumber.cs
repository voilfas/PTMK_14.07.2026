namespace Ticket.Domain.ValueObjects;

public record TicketNumber
{
    private string Number { get; }

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
    
    public override string ToString() => Number;
}