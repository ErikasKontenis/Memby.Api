using Memby.Contracts.Repositories;
using Memby.Data.DbContexts;
using Memby.Domain.Users;

namespace Memby.Data.Repositories
{
    public class UserRolesRepository : Repository<UserRole>, IUserRolesRepository
    {
        public UserRolesRepository(MembyDbContext dbContext)
            : base(dbContext)
        { }
    }
}
