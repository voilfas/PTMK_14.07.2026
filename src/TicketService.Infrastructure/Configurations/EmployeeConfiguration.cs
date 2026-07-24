using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketService.Domain.Entities;

namespace TicketService.Infrastructure.Configurations;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.ToTable("employees");
        
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .HasColumnName("id")
            .ValueGeneratedNever();

        builder.OwnsOne(e => e.FullName, fullName =>
        {
            fullName.WithOwner();
            
            fullName.Property(p => p.FirstName)
                .HasColumnName("first_name")
                .IsRequired()
                .HasMaxLength(20);
            
            fullName.Property(p => p.LastName)
                .HasColumnName("last_name")
                .IsRequired()
                .HasMaxLength(20);
            
            fullName.Property(p => p.Surname)
                .HasColumnName("surname")
                .IsRequired()
                .HasMaxLength(20);
        });
        
        builder.HasOne<Department>()
            .WithMany()
            .HasForeignKey(e => e.DepartmentId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(e => e.DepartmentId)
            .HasColumnName("department_id");
        
        builder.HasOne<Position>()
            .WithMany()
            .HasForeignKey(e => e.PositionId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(e => e.PositionId)
            .HasColumnName("position_id");

        builder.HasIndex(e => e.DepartmentId);
        
        builder.HasIndex(e => e.PositionId);
    }
}