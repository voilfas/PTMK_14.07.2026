using MediatR;
using TicketService.Application.DTOs.EmployeeDTOs;
using TicketService.Domain.Common;

namespace TicketService.Application.Features.Employees.Queries.GetEmployeeById;

public record GetEmployeeByIdQuery(
    Guid Id) : IRequest<Result<EmployeeDto>>;