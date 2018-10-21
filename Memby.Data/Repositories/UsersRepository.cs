using Memby.Contracts.Repositories;
using Memby.Data.DbContexts;
using Memby.Domain.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Memby.Data.Repositories
{
    public class UsersRepository : Repository<User>, IUsersRepository
    {
        public UsersRepository(MembyDbContext dbContext)
            : base(dbContext)
        { }

        public virtual async Task<User> GetAsync(Guid uuid)
        {
            return await _dbSet.FirstOrDefaultAsync(o => o.Uuid == uuid);
        }
    }
}
