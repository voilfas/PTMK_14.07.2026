using Microsoft.EntityFrameworkCore;
using TicketService.Domain.Entities;
using TicketService.Infrastructure;
using TicketService.Infrastructure.Persistence;

namespace TicketService.Seeder.Seeders;

public static class PositionSeeder
{
    public static async Task SeedAsync(ApplicationDbContext db)
    {
        var positionNames = new[]
        {
            "Программист",
            "Старший программист",
            "Тестировщик",
            "Дизайнер",
            "Системный администратор",
            "Юрист",
            "Бухгалтер",
            "HR-менеджер",
            "Руководитель отдела",
            "Аналитик"
        };
        
        if (await db.Positions.AnyAsync())
            return;

        var positions = positionNames
            .Select(name => Position.Create(name).Value)
            .ToList();

        await db.Positions.AddRangeAsync(positions);
        await db.SaveChangesAsync();
    }
}