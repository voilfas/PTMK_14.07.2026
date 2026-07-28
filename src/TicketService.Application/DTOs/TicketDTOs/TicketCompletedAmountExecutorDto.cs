namespace TicketService.Application.DTOs.TicketDTOs;

public record TicketCompletedAmountExecutorDto(
    Guid ExecutorId,
    string FullName,
    int AmountCompletedTickets);