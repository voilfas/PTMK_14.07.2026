using MediatR;
using TicketService.Domain.Common;

namespace TicketService.Application.UseCases.Departments.Commands.CreateDepartment;

public record CreateDepartmentCommand(
    string Name) :  IRequest<Result<Guid>>;