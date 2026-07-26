using MediatR;
using TicketService.Application.Abstractions.Persistence.Queries;
using TicketService.Application.Common;
using TicketService.Application.ResponceDTOs;

namespace TicketService.Application.UseCases.Employees.Queries.GetEmployees;

public class GetEmployeesHandler : IRequestHandler<GetEmployeesQuery, PageResult<EmployeeListItemDto>>
{
    private readonly IEmployeeReadRepository _repository;

    public GetEmployeesHandler(IEmployeeReadRepository repository)
    {
        _repository = repository;
    }

    
    public async Task<PageResult<EmployeeListItemDto>> Handle(
        GetEmployeesQuery query,
        CancellationToken cancellationToken)
    {
        return await _repository.GetAllAsync(query.Filter, cancellationToken);
    }
}