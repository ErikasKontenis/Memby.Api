using Memby.Contracts.Repositories;
using Memby.Data.DbContexts;
using Memby.Domain.Employees;

namespace Memby.Data.Repositories
{
    public class EmployeesRepository : Repository<Employee>, IEmployeesRepository
    {
        public EmployeesRepository(MembyDbContext dbContext)
            : base(dbContext)
        { }
    }
}
