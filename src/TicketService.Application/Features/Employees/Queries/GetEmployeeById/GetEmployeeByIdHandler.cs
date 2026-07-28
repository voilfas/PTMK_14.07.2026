using MediatR;
using TicketService.Application.Abstractions.Persistence.Queries;
using TicketService.Application.Common.Errors;
using TicketService.Application.DTOs.EmployeeDTOs;
using TicketService.Domain.Common;

namespace TicketService.Application.Features.Employees.Queries.GetEmployeeById;

public class GetEmployeeByIdHandler : IRequestHandler<GetEmployeeByIdQuery, Result<EmployeeDto>>
{
    private readonly IEmployeeReadRepository _repository;

    public GetEmployeeByIdHandler(IEmployeeReadRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<EmployeeDto>> Handle(
        GetEmployeeByIdQuery request,
        CancellationToken cancellationToken)
    {
        var employeeDto = await _repository.GetByIdAsync(request.Id, cancellationToken);

        if (employeeDto is null)
            return Result<EmployeeDto>.Failure(ErrorsEmployee.NotFoundById);
        
        return Result<EmployeeDto>.Success(employeeDto);
    } 
}