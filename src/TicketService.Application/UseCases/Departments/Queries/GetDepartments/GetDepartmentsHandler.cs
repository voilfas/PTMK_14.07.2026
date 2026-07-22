using TicketService.Application.Abstractions.Persistence.Queries;
using TicketService.Application.Common;
using TicketService.Application.DTOs;

namespace TicketService.Application.UseCases.Departments.Queries.GetDepartments;

public class GetDepartmentsHandler
{
    private readonly IDepartmentReadRepository _repository;

    public GetDepartmentsHandler(IDepartmentReadRepository repository)
    {
        _repository = repository;
    }

    public async Task<PageResult<DepartmentDto>> Handle(
        GetDepartmentsQuery query,
        CancellationToken cancellationToken)
    {
        return await _repository.GetAllAsync(query.Page, query.PageSize, cancellationToken);
    }
}