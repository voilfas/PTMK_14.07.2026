using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketService.Domain.Entities;
using TicketService.Domain.ValueObjects;

namespace TicketService.Infrastructure.Configurations;

public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
{
    public void Configure(EntityTypeBuilder<Ticket> builder)
    {
        builder.ToTable("tickets");
        
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id)
            .ValueGeneratedNever();

        /*builder.OwnsOne(t => t.TicketNumber, ticketNumber =>
        {
            ticketNumber.WithOwner();
            
            ticketNumber.Property(n => n.Number)
                .IsRequired()
                .HasMaxLength(50);
        });*/

        builder.Property(t => t.TicketNumber)
            .HasConversion(
                vo => vo.Number,
                dbValue => TicketNumber.FromDatabase(dbValue)
            )
            .HasColumnName("ticket_number")
            .HasColumnType("char(33)")
            .IsRequired();
        
        builder.HasIndex(t => t.TicketNumber)
            .IsUnique();
        
        builder.Property(t => t.CreatedAt)
            .IsRequired();
        
        builder.HasOne<Employee>()
            .WithMany()
            .HasForeignKey(t => t.AuthorId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.Property(t => t.Description)
            .IsRequired()
            .HasMaxLength(300);
        
        builder.Property(t => t.Status)
            .HasConversion<string>()
            .IsRequired();
        
        builder.Property(t => t.Type)
            .HasConversion<string>()
            .IsRequired();
        
        builder.Property(t => t.Deadline)
            .IsRequired();
        
        builder.HasMany(t => t.Executors)
            .WithOne()
            .HasForeignKey(te => te.TicketId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Navigation(t => t.Executors)
            .UsePropertyAccessMode(PropertyAccessMode.Field);
        
        builder.HasIndex(x => x.AuthorId);

        builder.HasIndex(x => x.Status);

        builder.HasIndex(x => x.Type);

        builder.HasIndex(x => x.CreatedAt);

        builder.HasIndex(x => x.Deadline);
    }
}