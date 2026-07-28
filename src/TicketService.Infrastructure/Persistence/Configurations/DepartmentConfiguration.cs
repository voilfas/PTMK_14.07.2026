using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketService.Domain.Entities;
using TicketService.Domain.ValueObjects;

namespace TicketService.Infrastructure.Persistence.Configurations;

public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        builder.ToTable("departments");
        
        builder.HasKey(d => d.Id);
        
        builder.Property(d => d.Id)
            .HasColumnName("id")
            .ValueGeneratedNever();
        
        builder.Property(d => d.Name)
            .HasColumnName("name")
            .IsRequired()
            .HasMaxLength(30);
        
        builder.Property(d => d.Code)
            .HasConversion(
                code => code.Value,
                value => CodeDepartment.FromDatabase(value))
            .HasColumnName("code")
            .HasMaxLength(15)
            .IsRequired();

        builder.Property(d => d.IsActive)
            .HasColumnName("is_active")
            .IsRequired();
        
        builder.HasIndex(d => d.Code).IsUnique();
    }
}