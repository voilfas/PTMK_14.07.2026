using MediatR;
using TicketService.Application.Abstractions.Cache;
using TicketService.Application.Abstractions.Persistence.Queries;
using TicketService.Application.Common.Pagination;
using TicketService.Application.DTOs.DepartmentDTOs;

namespace TicketService.Application.Features.Departments.Queries.GetDepartments;

public class GetDepartmentsHandler
    : IRequestHandler<GetDepartmentsQuery, PageResult<DepartmentDto>>
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
        GetDepartmentsQuery request,
        CancellationToken cancellationToken)
    {
        var cacheKey =  $"departments:{request.Page}:{request.PageSize}";
        
        var cachedDepartments  = await _cache.GetAsync<PageResult<DepartmentDto>>(cacheKey);

        if (cachedDepartments  is not null)
            return cachedDepartments ;
        
        var departments =  await _repository.GetAllAsync(request.Page, request.PageSize, cancellationToken);
        
        await _cache.SetAsync(cacheKey,  departments, TimeSpan.FromMinutes(5));
        
        return departments;
    }
}