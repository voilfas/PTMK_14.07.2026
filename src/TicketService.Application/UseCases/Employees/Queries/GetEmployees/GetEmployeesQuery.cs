using MediatR;
using TicketService.Application.Common;
using TicketService.Application.ResponceDTOs;

namespace TicketService.Application.UseCases.Employees.Queries.GetEmployees;

public record GetEmployeesQuery(EmployeeFilter Filter) :  IRequest<PageResult<EmployeeListItemDto>>;