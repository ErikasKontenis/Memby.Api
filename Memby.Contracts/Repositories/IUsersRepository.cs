using Memby.Domain.Users;
using System;
using System.Threading.Tasks;

namespace Memby.Contracts.Repositories
{
    public interface IUsersRepository : IRepository<User>
    {
        Task<User> GetAsync(Guid uuid);
    }
}
