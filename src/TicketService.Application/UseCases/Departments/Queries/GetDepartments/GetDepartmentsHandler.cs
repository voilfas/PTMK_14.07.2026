using MediatR;
using Microsoft.Extensions.Logging;
using TicketService.Application.Abstractions.Persistence.Queries;
using TicketService.Application.Common;
using TicketService.Application.ResponceDTOs;

namespace TicketService.Application.UseCases.Departments.Queries.GetDepartments;

public class GetDepartmentsHandler : IRequestHandler<GetDepartmentsQuery, PageResult<DepartmentDto>>
{
    private readonly IDepartmentReadRepository _repository;
    private readonly ILogger<GetDepartmentsHandler> _logger;

    public GetDepartmentsHandler(
        IDepartmentReadRepository repository,
        ILogger<GetDepartmentsHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<PageResult<DepartmentDto>> Handle(
        GetDepartmentsQuery query,
        CancellationToken cancellationToken)
    {
        /*_logger.LogInformation(
            "Getting departments. Page={Page}, PageSize={PageSize}",
            query.Page,
            query.PageSize);*/
        
        var departments =  await _repository.GetAllAsync(query.Page, query.PageSize, cancellationToken);
        
        /*_logger.LogInformation(
            "Retrieved {Count} departments",
            departments.TotalCount);*/
        
        return departments;
    }
}