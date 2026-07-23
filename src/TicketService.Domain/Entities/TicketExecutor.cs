namespace TicketService.Domain.Entities;

public class TicketExecutor
{
    public Guid TicketId { get; private set; }
    public Guid EmployeeId { get; private set; }

    private TicketExecutor()
    {
    }

    public TicketExecutor(Guid ticketId, Guid employeeId)
    {
        TicketId = ticketId;
        EmployeeId = employeeId;
    }
}