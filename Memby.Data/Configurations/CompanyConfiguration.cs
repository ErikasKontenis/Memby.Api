using Memby.Domain.Companies;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Memby.Data.Configurations
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> entity)
        {
            entity.HasKey(e => new { e.Id });

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd();

            entity.HasOne(o => o.User)
                .WithMany(o => o.Companies)
                .HasForeignKey(o => o.UserId);

            entity.HasMany(o => o.Employees)
                .WithOne(o => o.Company)
                .HasForeignKey(o => o.CompanyId);
        }
    }
}
