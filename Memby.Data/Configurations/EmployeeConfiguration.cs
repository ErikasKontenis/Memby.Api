using Memby.Domain.Employees;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Memby.Data.Configurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> entity)
        {
            entity.HasKey(e => new { e.Id });

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd();

            entity.HasOne(o => o.User)
                .WithMany(o => o.Employees)
                .HasForeignKey(o => o.UserId);

            entity.HasOne(o => o.Company)
                .WithMany(o => o.Employees)
                .HasForeignKey(o => o.CompanyId);
        }
    }
}
