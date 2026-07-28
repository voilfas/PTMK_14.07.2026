using MediatR;
using TicketService.Application.Common.Pagination;
using TicketService.Application.DTOs.DepartmentDTOs;

namespace TicketService.Application.Features.Departments.Queries.GetDepartments;

public record GetDepartmentsQuery() : PageQuery, IRequest<PageResult<DepartmentDto>>;