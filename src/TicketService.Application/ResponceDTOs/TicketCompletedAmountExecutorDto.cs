namespace TicketService.Application.ResponceDTOs;

public record TicketCompletedAmountExecutorDto(
    Guid ExecutorId,
    string FullName,
    int AmountCompletedTickets);