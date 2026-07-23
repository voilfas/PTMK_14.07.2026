using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketService.Domain.Entities;
using TicketService.Domain.ValueObjects;

namespace TicketService.Infrastructure.Configurations;

public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        builder.ToTable("departments");
        
        builder.HasKey(d => d.Id);
        
        builder.Property(d => d.Id)
            .ValueGeneratedNever();
        
        builder.Property(d => d.Name)
            .IsRequired()
            .HasMaxLength(30);
        
        builder.Property(d => d.Code)
            .HasConversion(
                code => code.Code,
                value => CodeDepartment.FromDatabase(value))
            .HasColumnName("code")
            .HasMaxLength(15)
            .IsRequired();

        builder.Property(d => d.IsActive)
            .IsRequired();
        
        builder.HasIndex(d => d.Code).IsUnique();
    }
}