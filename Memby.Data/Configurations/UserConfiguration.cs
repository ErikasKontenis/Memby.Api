using Memby.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Memby.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> entity)
        {
            entity.HasKey(e => new { e.Id });

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd();

            entity.HasIndex(e => e.Email)
                .IsUnique();

            entity.HasIndex(o => new { o.Email, o.Password })
                .IsUnique();

            entity.HasMany(o => o.UserProviders)
                .WithOne(o => o.User)
                .HasForeignKey(o => o.UserId);

            entity.HasMany(o => o.Employees)
                .WithOne(o => o.User)
                .HasForeignKey(o => o.UserId);
        }
    }
}
