using MediatR;
using TicketService.Application.Abstractions.Persistence.Queries;
using TicketService.Application.Common.Pagination;
using TicketService.Application.DTOs.EmployeeDTOs;

namespace TicketService.Application.Features.Employees.Queries.GetEmployees;

public class GetEmployeesHandler : IRequestHandler<GetEmployeesQuery, PageResult<EmployeeListItemDto>>
{
    private readonly IEmployeeReadRepository _repository;

    public GetEmployeesHandler(IEmployeeReadRepository repository)
    {
        _repository = repository;
    }

    
    public async Task<PageResult<EmployeeListItemDto>> Handle(
        GetEmployeesQuery request,
        CancellationToken cancellationToken)
    {
        return await _repository.GetAllAsync(request.Filter, cancellationToken);
    }
}