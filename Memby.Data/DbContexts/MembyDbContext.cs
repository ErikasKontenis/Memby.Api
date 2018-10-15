using Memby.Data.Configurations;
using Memby.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Memby.Data.DbContexts
{
    public class MembyDbContext : DbContext
    {
        public MembyDbContext(DbContextOptions<MembyDbContext> options) : base(options)
        { }

        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }
    }
}
