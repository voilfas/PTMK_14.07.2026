using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketService.Domain.Entities;

namespace TicketService.Infrastructure.Persistence.Configurations;

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

        builder.HasOne(te => te.Employee)
            .WithMany()
            .HasForeignKey(te => te.EmployeeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(t => t.TicketId)
            .HasColumnName("ticket_id");

        builder.HasOne(te => te.Ticket)
            .WithMany(t => t.Executors)
            .HasForeignKey(te => te.TicketId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(t => t.EmployeeId)
            .HasColumnName("employee_id");
    }
}