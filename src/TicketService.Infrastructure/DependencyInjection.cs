using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TicketService.Application.Abstractions;
using TicketService.Application.Abstractions.Persistence.Commands;
using TicketService.Application.Abstractions.Persistence.Queries;
using TicketService.Application.Common.Cache;
using TicketService.Infrastructure.Persistence;
using TicketService.Infrastructure.Persistence.Repositories;
using TicketService.Infrastructure.Services;

namespace TicketService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionStringPostgre = configuration.GetConnectionString("Database")
                               ?? throw new InvalidOperationException(
                                   "Connection string 'Database' was not found.");
        
        var connectionStringRedis = configuration.GetConnectionString("Redis")
                                    ?? throw new InvalidOperationException(
                                        "Connection string 'Redis' was not found.");
        
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(connectionStringPostgre);
        });

        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = connectionStringRedis;
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
        
        // ------

        services.AddScoped<ICacheService, CacheServiceRedis>();
        
        return services;
    }
}