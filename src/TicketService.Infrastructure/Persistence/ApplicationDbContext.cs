using Microsoft.EntityFrameworkCore;
using TicketService.Domain.Entities;

namespace TicketService.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public DbSet<Ticket>  Tickets => Set<Ticket>();
    public DbSet<Department> Departments => Set<Department>();
    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<Position> Positions => Set<Position>();
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}