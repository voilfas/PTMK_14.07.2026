using Microsoft.EntityFrameworkCore;
using TicketService.Domain.Entities;
using TicketService.Domain.ValueObjects;
using TicketService.Infrastructure;
using TicketService.Infrastructure.Persistence;

namespace TicketService.Seeder.Seeders;

public static class EmployeeSeeder
{
    public static async Task SeedAsync(
        ApplicationDbContext db,
        int count)
    {
        if (await db.Employees.AnyAsync())
            return;
        
        var departments = await db.Departments.ToListAsync();
        var positions = await db.Positions.ToListAsync();
        
        var random = new Random();
        
        var firstNames = new[]
        {
            "Артем",
            "Иван",
            "Олег",
            "Максим",
            "Сергей",
            "Павел",
            "Дмитрий",
            "Александр",
            "Владимир",
            "Андрей"
        };
        
        var lastNames = new[]
        {
            "Иванович",
            "Петрович",
            "Александрович",
            "Дмитриевич",
            "Сергеевич",
            "Олегович"
        };
        
        var surnames = new[]
        {
            "Иванов",
            "Петров",
            "Сидоров",
            "Смирнов",
            "Орлов",
            "Кузнецов",
            "Собкалов",
            "Ильин"
        };
        
        var employees = new List<Employee>();

        for (int i = 0; i < count; i++)
        {
            var fullName = FullName.Create(
                firstNames[random.Next(firstNames.Length)],
                lastNames[random.Next(lastNames.Length)],
                surnames[random.Next(surnames.Length)]
            ).Value;

            var department = departments[random.Next(departments.Count)];

            var position = positions[random.Next(positions.Count)];

            var employee = Employee.Create(
                fullName,
                department.Id,
                position.Id
            ).Value;

            employees.Add(employee);
        }
        
        await db.Employees.AddRangeAsync(employees);
        await db.SaveChangesAsync();
    }
}