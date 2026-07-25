namespace TicketService.Domain.Entities;

public class TicketExecutor
{
    public Guid TicketId { get; private set; }
    public Ticket Ticket { get; private set; } = null!;
    public Guid EmployeeId { get; private set; }
    public Employee Employee { get; private set; } = null!;

    private TicketExecutor()
    {
    }

    public TicketExecutor(Guid ticketId, Guid employeeId)
    {
        TicketId = ticketId;
        EmployeeId = employeeId;
    }
}