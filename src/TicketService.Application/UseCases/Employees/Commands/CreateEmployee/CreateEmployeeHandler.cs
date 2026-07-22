using TicketService.Application.Abstractions;
using TicketService.Application.Abstractions.Persistence.Commands;
using TicketService.Application.Common.ErrorsHandler;
using TicketService.Domain.Common;
using TicketService.Domain.Entities;
using TicketService.Domain.ValueObjects;

namespace TicketService.Application.UseCases.Employees.Commands.CreateEmployee;

public class CreateEmployeeHandler
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDepartmentRepository _departmentRepository;
    private readonly IPositionRepository _positionRepository;

    public CreateEmployeeHandler(
        IEmployeeRepository employeeRepository,
        IUnitOfWork unitOfWork,
        IDepartmentRepository departmentRepository,
        IPositionRepository positionRepository)
    {
        _employeeRepository = employeeRepository;
        _unitOfWork = unitOfWork;
        _departmentRepository = departmentRepository;
        _positionRepository = positionRepository;
    }

    public async Task<Result<Guid>> Handle(
        CreateEmployeeCommand command,
        CancellationToken cancellationToken)
    {
        if (!await _departmentRepository.ExistsAsync(command.DepartmentId, cancellationToken))
            return Result<Guid>.Failure(ErrorsDepartment.NotFoundById);
        
        if (!await _positionRepository.ExistsAsync(command.PositionId, cancellationToken))
            return Result<Guid>.Failure(ErrorsPosition.NotFoundById);
        
        var fullNameResult = FullName.Create(command.FirstName, command.LastName, command.Surname);
        if (fullNameResult.IsFailure)
            return Result<Guid>.Failure(fullNameResult.Error);
        
        var employeeResult = Employee.Create(
            fullNameResult.Value,
            command.DepartmentId,
            command.PositionId);

        if (employeeResult.IsFailure)
            return Result<Guid>.Failure(employeeResult.Error);
        
        var employee = employeeResult.Value;
        
        await _employeeRepository.AddAsync(employee, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Result<Guid>.Success(employee.Id);
    }
}