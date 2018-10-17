using Memby.Contracts.Repositories;
using Memby.Data.DbContexts;
using Memby.Domain.Users;

namespace Memby.Data.Repositories
{
    public class UsersRepository : Repository<User>, IUsersRepository
    {
        public UsersRepository(MembyDbContext dbContext)
            : base(dbContext)
        { }
    }
}
