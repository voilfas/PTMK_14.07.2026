using TicketService.Application.Abstractions.Persistence.Queries;
using TicketService.Application.Common;
using TicketService.Application.DTOs;

namespace TicketService.Application.UseCases.Employees.Queries.GetEmployees;

public class GetEmployeesHandler
{
    private readonly IEmployeeReadRepository _repository;

    public GetEmployeesHandler(IEmployeeReadRepository repository)
    {
        _repository = repository;
    }

    public async Task<PageResult<EmployeeDto>> Handle(
        GetEmployeesQuery query,
        CancellationToken cancellationToken)
    {
        return await _repository.GetAllAsync(query.Filter, cancellationToken);
    }
}