using TicketService.Application.Abstractions;
using TicketService.Application.Abstractions.Persistence;
using TicketService.Application.Abstractions.Persistence.Commands;
using TicketService.Domain.Common;
using TicketService.Domain.Entities;

namespace TicketService.Application.UseCases.Departments.Commands.CreateDepartment;

public class CreateDepartmentHandler
{
    private readonly IDepartmentRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateDepartmentHandler(IDepartmentRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(
        CreateDepartmentCommand command,
        CancellationToken cancellationToken)
    {
        var departmentResult = Department.Create(
            command.Name,
            command.Code);

        if (departmentResult.IsFailure)
            return Result<Guid>.Failure(departmentResult.Error);
        
        var department = departmentResult.Value;

        await _repository.AddAsync(department, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result<Guid>.Success(department.Id);
    }
}