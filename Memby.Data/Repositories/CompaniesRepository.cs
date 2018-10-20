using Memby.Contracts.Repositories;
using Memby.Data.DbContexts;
using Memby.Domain.Companies;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Memby.Data.Repositories
{
    public class CompaniesRepository : Repository<Company>, ICompaniesRepository
    {
        public CompaniesRepository(MembyDbContext dbContext)
            : base(dbContext)
        { }

        public async Task<Company> GetAsync(int id, int userId)
        {
            return await _dbSet.FirstOrDefaultAsync(o => o.Id == id && o.UserId == userId);
        }
    }
}
