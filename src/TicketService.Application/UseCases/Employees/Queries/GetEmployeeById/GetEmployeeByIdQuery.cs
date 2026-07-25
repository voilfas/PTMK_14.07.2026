using MediatR;
using TicketService.Application.DTOs;
using TicketService.Domain.Common;

namespace TicketService.Application.UseCases.Employees.Queries.GetEmployeeById;

public record GetEmployeeByIdQuery(
    Guid Id) : IRequest<Result<EmployeeDto>>;