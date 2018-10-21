using Memby.Application.Employees;
using System.Threading.Tasks;

namespace Memby.Contracts.Services
{
    public interface IEmployeesService
    {
        Task<EmployeeDto> Get(int id, int userId);

        Task<CreateEmployeeResultDto> Create(CreateEmployeeDto createEmployeeDto, int userId);

        Task<UpdateEmployeeResultDto> Update(UpdateEmployeeDto updateEmployeeDto, int userId);

        Task<DeleteEmployeeResultDto> Delete(int id, int userId);
    }
}
