using Memby.Domain.Companies;
using System.Threading.Tasks;

namespace Memby.Contracts.Repositories
{
    public interface ICompaniesRepository : IRepository<Company>
    {
        Task<Company> GetAsync(int id, int userId);
    }
}
