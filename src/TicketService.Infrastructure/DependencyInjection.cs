using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TicketService.Application.Abstractions;
using TicketService.Application.Abstractions.Persistence.Commands;
using TicketService.Application.Abstractions.Persistence.Queries;
using TicketService.Infrastructure.Persistence;
using TicketService.Infrastructure.Persistence.Repositories;

namespace TicketService.Infrastructure.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Database")
                               ?? throw new InvalidOperationException(
                                   "Connection string 'Database' was not found.");
        
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });
        
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        services.AddScoped<IDepartmentRepository, DepartmentRepository>();
        services.AddScoped<IDepartmentReadRepository, DepartmentReadRepository>();
        
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<IEmployeeReadRepository, EmployeeReadRepository>();
        
        services.AddScoped<IPositionRepository, PositionRepository>();
        services.AddScoped<IPositionReadRepository, PositionReadRepository>();
        
        services.AddScoped<ITicketRepository, TicketRepository>();
        services.AddScoped<ITicketReadRepository, TicketReadRepository>();
        
        return services;
    }
}