namespace TicketService.API.RequestDTOs.Tickets;

public record AddExecutorsRequest(
    IReadOnlyCollection<Guid> ExecutorsIds
    );