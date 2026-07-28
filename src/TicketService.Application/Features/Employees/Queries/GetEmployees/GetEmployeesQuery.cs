using MediatR;
using TicketService.Application.Common.Pagination;
using TicketService.Application.DTOs.EmployeeDTOs;

namespace TicketService.Application.Features.Employees.Queries.GetEmployees;

public record GetEmployeesQuery(EmployeeFilter Filter) : 
    IRequest<PageResult<EmployeeListItemDto>>;