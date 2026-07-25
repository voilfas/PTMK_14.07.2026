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
            .HasColumnName("id")
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
            .HasColumnName("created_at")
            .IsRequired();
        
        builder.HasOne(t => t.Author)
            .WithMany()
            .HasForeignKey(t => t.AuthorId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(t => t.AuthorId)
            .HasColumnName("author_id");
        
        builder.Property(t => t.Description)
            .HasColumnName("description")
            .IsRequired()
            .HasMaxLength(300);
        
        builder.Property(t => t.Status)
            .HasColumnName("status")
            .HasConversion<string>()
            .IsRequired();
        
        builder.Property(t => t.Type)
            .HasColumnName("type")
            .HasConversion<string>()
            .IsRequired();
        
        builder.Property(t => t.Deadline)
            .HasColumnName("deadline")
            .IsRequired();
        
        builder.HasMany(t => t.Executors)
            .WithOne(te => te.Ticket)
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