using Microsoft.EntityFrameworkCore;
using TicketService.Domain.Entities;
using TicketService.Domain.ValueObjects;
using TicketService.Infrastructure;
using TicketService.Infrastructure.Persistence;

namespace TicketService.Seeder.Seeders;

public static class DepartmentSeeder
{
    public static async Task SeedAsync(ApplicationDbContext db)
    {
        if (await db.Departments.AnyAsync())
            return;

        var departments = new List<Department>();

        CodeDepartment? lastCode = null;

        var departmentNames = new[]
        {
            "Информационный отдел",
            "Юридический отдел",
            "Бухгалтерия",
            "Отдел кадров"
        };

        foreach (var name in departmentNames)
        {
            lastCode = CodeDepartment.GenerateNext(lastCode);

            var department = Department.Create(name, lastCode).Value;

            departments.Add(department);
        }

        await db.Departments.AddRangeAsync(departments);
        await db.SaveChangesAsync();
    }
}