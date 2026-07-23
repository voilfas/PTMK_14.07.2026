using MediatR;
using TicketService.Application.Common;
using TicketService.Application.DTOs;

namespace TicketService.Application.UseCases.Departments.Queries.GetDepartments;

public record GetDepartmentsQuery() : PageQuery, IRequest<PageResult<DepartmentDto>>;