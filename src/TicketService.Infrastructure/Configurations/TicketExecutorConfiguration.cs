using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketService.Domain.Entities;

namespace TicketService.Infrastructure.Configurations;

public class TicketExecutorConfiguration : IEntityTypeConfiguration<TicketExecutor>
{
    public void Configure(EntityTypeBuilder<TicketExecutor> builder)
    {
        builder.ToTable("ticket_executors");

        builder.HasKey(x => new
        {
            x.TicketId,
            x.EmployeeId
        });

        builder.HasOne<Employee>()
            .WithMany()
            .HasForeignKey(x => x.EmployeeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<Ticket>()
            .WithMany(t => t.Executors)
            .HasForeignKey(x => x.TicketId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}