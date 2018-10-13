using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Memby.Data.DbContexts
{
    public class MembyDbContext : DbContext
    {
        public MembyDbContext(DbContextOptions<MembyDbContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
