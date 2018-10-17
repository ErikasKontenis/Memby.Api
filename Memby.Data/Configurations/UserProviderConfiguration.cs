using Memby.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Memby.Data.Configurations
{
    public class UserProviderConfiguration : IEntityTypeConfiguration<UserProvider>
    {
        public void Configure(EntityTypeBuilder<UserProvider> entity)
        {
            entity.HasKey(e => new { e.Id });

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd();

            entity.HasIndex(e => e.Uuid)
                .IsUnique();

            entity.HasOne(o => o.User)
                .WithMany(o => o.UserProviders);
        }
    }
}
