using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketService.Domain.Entities;

namespace TicketService.Infrastructure.Configurations;

public class PositionConfiguration : IEntityTypeConfiguration<Position>
{
    public void Configure(EntityTypeBuilder<Position> builder)
    {
        builder.ToTable("positions");
        
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .ValueGeneratedNever();
        
        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(30);
        
        builder.Property(p => p.IsActive)
            .IsRequired();
        
        builder.HasIndex(p => p.Name).IsUnique();
    }
}