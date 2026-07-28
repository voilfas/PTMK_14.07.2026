using Microsoft.EntityFrameworkCore;
using TicketService.Domain.Entities;
using TicketService.Domain.Enums;
using TicketService.Domain.ValueObjects;
using TicketService.Infrastructure;
using TicketService.Infrastructure.Persistence;

namespace TicketService.Seeder.Seeders;

public static class TicketSeeder
{
    public static async Task SeedAsync(
        ApplicationDbContext db,
        int count)
    {
        if (await db.Tickets.AnyAsync())
            return;
     
        var descriptions = new []
        {
            "Не работает компьютер",
            "Не работает принтер",
            "Создать сайт",
            "Настроить почту",
            "Заменить картридж",
            "Установить Windows",
            "Настроить VPN",
            "Подключить монитор"
        };
        
        var random = new Random();
        
        var employees = await db.Employees
            .Select(e => e.Id)
            .ToListAsync();
        
        const int batchSize = 5000;
        
        var tickets = new List<Ticket>(batchSize);
        
        for (int i = 0; i < count; i++)
        {
            var authorId = employees[random.Next(employees.Count)];
            
            var createdAt =
                DateTime.UtcNow.AddDays(-random.Next(0, 730));
            
            var type = (TicketType)random.Next(3);
            
            var executorCount = random.Next(1, 4);

            var executors = new HashSet<Guid>();

            var description = descriptions[random.Next(descriptions.Length)];

            while (executors.Count < executorCount)
            {
                var id = employees[random.Next(employees.Count)];

                if (id != authorId)
                    executors.Add(id);
            }
            
            var result = Ticket.Create(
                authorId,
                executors,
                description,
                type,
                createdAt);
            
            if (result.IsFailure)
                continue;
            
            var ticket = result.Value;
            
            var chance = random.Next(100);

            if (chance < 15)
            {
            }
            else if (chance < 25)
            {
                ticket.ChangeStatus(TicketStatus.AwaitingApproval);
            }
            else if (chance < 35)
            {
                ticket.ChangeStatus(TicketStatus.AwaitingApproval);
                ticket.ChangeStatus(TicketStatus.Approved);
            }
            else if (chance < 70)
            {
                ticket.ChangeStatus(TicketStatus.AwaitingApproval);
                ticket.ChangeStatus(TicketStatus.Approved);
                ticket.ChangeStatus(TicketStatus.InProgress);
            }
            else
            {
                ticket.ChangeStatus(TicketStatus.AwaitingApproval);
                ticket.ChangeStatus(TicketStatus.Approved);
                ticket.ChangeStatus(TicketStatus.InProgress);
                ticket.ChangeStatus(TicketStatus.Completed);
            }
            
            tickets.Add(ticket);
            
            if (tickets.Count >= batchSize)
            {
                await db.Tickets.AddRangeAsync(tickets);

                await db.SaveChangesAsync();

                db.ChangeTracker.Clear();

                tickets.Clear();

                Console.WriteLine($"Created {i + 1:N0}/{count:N0} tickets...");
            }
        }

        if (tickets.Count > 0)
        {
            await db.Tickets.AddRangeAsync(tickets);
            await db.SaveChangesAsync();

            db.ChangeTracker.Clear();
        }
    }
}