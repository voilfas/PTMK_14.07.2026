using MediatR;
using TicketService.Application.Common;
using TicketService.Application.DTOs;

namespace TicketService.Application.UseCases.Employees.Queries.GetEmployees;

public record GetEmployeesQuery(EmployeeFilter Filter) :  IRequest<PageResult<EmployeeDto>>;