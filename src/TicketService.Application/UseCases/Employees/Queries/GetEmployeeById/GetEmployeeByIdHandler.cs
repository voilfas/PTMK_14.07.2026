using MediatR;
using TicketService.Application.Abstractions.Persistence.Queries;
using TicketService.Application.Common.ErrorsHandler;
using TicketService.Application.DTOs;
using TicketService.Domain.Common;

namespace TicketService.Application.UseCases.Employees.Queries.GetEmployeeById;

public class GetEmployeeByIdHandler : IRequestHandler<GetEmployeeByIdQuery, Result<EmployeeDto>>
{
    private readonly IEmployeeReadRepository _repository;

    public GetEmployeeByIdHandler(IEmployeeReadRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<EmployeeDto>> Handle(GetEmployeeByIdQuery query, CancellationToken cancellationToken)
    {
        var employeeDto = await _repository.GetByIdAsync(query.Id, cancellationToken);

        if (employeeDto is null)
            return Result<EmployeeDto>.Failure(ErrorsEmployee.NotFoundById);
        
        return Result<EmployeeDto>.Success(employeeDto);
    } 
}