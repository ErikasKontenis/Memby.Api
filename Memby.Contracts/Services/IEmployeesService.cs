using Memby.Application.Employees;
using System.Threading.Tasks;

namespace Memby.Contracts.Services
{
    public interface IEmployeesService
    {
        Task<EmployeeDto> Get(int id, int userId);

        Task<CreateEmployeeResultDto> Create(CreateEmployeeDto createEmployeeDto, int userId);
    }
}
