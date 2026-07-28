using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TicketService.Infrastructure;
using TicketService.Infrastructure.Persistence;
using TicketService.Seeder.Seeders;

//TRUNCATE TABLE employees CASCADE;

/*TRUNCATE TABLE
   ticket_executors,
   tickets,
   employees,
   departments,
   positions
RESTART IDENTITY CASCADE;*/

var services = new ServiceCollection();

var configuration = new ConfigurationBuilder()
   .SetBasePath(Path.GetFullPath(Path.Combine(
      AppContext.BaseDirectory,
      "..", "..", "..", "..",
      "TicketService.API")))
   .AddJsonFile("appsettings.Development.json")
   .Build();

var connectionString = configuration.GetConnectionString("Database")
                       ?? throw new InvalidOperationException("Connection string 'TicketService' not found.");

services.AddDbContext<ApplicationDbContext>(options =>
{
   options.UseNpgsql(connectionString);
});

var provider = services.BuildServiceProvider();

using var scope = provider.CreateScope();

var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

Console.WriteLine("Database connected");

if (args.Contains("clean", StringComparer.OrdinalIgnoreCase))
{
   await db.Database.ExecuteSqlRawAsync("""
                                        TRUNCATE TABLE
                                            ticket_executors,
                                            tickets,
                                            employees,
                                            departments,
                                            positions
                                        RESTART IDENTITY CASCADE;
                                        """);

   Console.WriteLine("Database cleaned.");

   return;
}

int ticketsCount = 1000;

foreach (var arg in args)
{
   if (int.TryParse(arg, out var value))
   {
      ticketsCount = value;
      break;
   }
}

Console.WriteLine("Seeding PLEASE wait..");

await DepartmentSeeder.SeedAsync(db);
var departments = await db.Departments.CountAsync();

Console.WriteLine($"Departments: {departments}");

await PositionSeeder.SeedAsync(db);
var positions = await db.Positions.CountAsync();

Console.WriteLine($"Positions: {positions}");

await EmployeeSeeder.SeedAsync(db, 1000);
var employees = await db.Employees.CountAsync();

Console.WriteLine($"Employees: {employees}");

await TicketSeeder.SeedAsync(db, /*1000000*/ ticketsCount);
var tickets = await db.Tickets.CountAsync();

Console.WriteLine($"Tickets: {tickets}");