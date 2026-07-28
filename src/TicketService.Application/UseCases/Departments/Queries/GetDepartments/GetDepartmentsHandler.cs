using MediatR;
using Microsoft.Extensions.Logging;
using TicketService.Application.Abstractions.Persistence.Queries;
using TicketService.Application.Common;
using TicketService.Application.Common.Cache;
using TicketService.Application.ResponceDTOs;

namespace TicketService.Application.UseCases.Departments.Queries.GetDepartments;

public class GetDepartmentsHandler : IRequestHandler<GetDepartmentsQuery, PageResult<DepartmentDto>>
{
    private readonly IDepartmentReadRepository _repository;
    private readonly ICacheService _cache;

    public GetDepartmentsHandler(
        IDepartmentReadRepository repository,
        ICacheService cache)
    {
        _repository = repository;
        _cache = cache;
    }

    public async Task<PageResult<DepartmentDto>> Handle(
        GetDepartmentsQuery query,
        CancellationToken cancellationToken)
    {

        var cacheKey =  $"departments:{query.Page}:{query.PageSize}";
        
        var cachedDepartments  = await _cache.GetAsync<PageResult<DepartmentDto>>(cacheKey);

        if (cachedDepartments  is not null)
            return cachedDepartments ;
        
        var departments =  await _repository.GetAllAsync(query.Page, query.PageSize, cancellationToken);
        
        await _cache.SetAsync(cacheKey,  departments, TimeSpan.FromMinutes(5));
        
        return departments;
    }
}