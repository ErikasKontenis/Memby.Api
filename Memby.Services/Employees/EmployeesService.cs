using Memby.Application.Employees;
using Memby.Contracts.Repositories;
using Memby.Contracts.Services;
using Memby.Core.Enums;
using Memby.Core.Exceptions;
using Memby.Core.Resources;
using Memby.Domain.Employees;
using Memby.Domain.Users;
using System.Threading.Tasks;

namespace Memby.Services.Employees
{
    public class EmployeesService : IEmployeesService
    {
        private readonly IEmployeesRepository _employeesRepository;
        private readonly ICompaniesRepository _companiesRepository;
        private readonly IUsersRepository _usersRepository;
        private readonly IUserRolesRepository _userRolesRepository;

        public EmployeesService(IEmployeesRepository employeesRepository,
            ICompaniesRepository companiesRepository,
            IUsersRepository usersRepository,
            IUserRolesRepository userRolesRepository)
        {
            _employeesRepository = employeesRepository;
            _companiesRepository = companiesRepository;
            _usersRepository = usersRepository;
            _userRolesRepository = userRolesRepository;
        }

        public async Task<EmployeeDto> Get(int id, int userId)
        {
            var employee = await _employeesRepository.GetAsync(id);
            if (employee == null)
            {
                throw new ValidationException(Messages.EmployeeDoesNotExist, nameof(Messages.EmployeeDoesNotExist));
            }

            var company = await _companiesRepository.GetAsync(employee.CompanyId, userId);
            if (company == null)
            {
                throw new ValidationException(Messages.EmployeeDoesNotExist, nameof(Messages.EmployeeDoesNotExist));
            }

            var employeeDto = new EmployeeDto()
            {
                CompanyId = employee.CompanyId,
                Position = employee.Position
            };

            return employeeDto;
        }

        public async Task<CreateEmployeeResultDto> Create(CreateEmployeeDto createEmployeeDto, int userId)
        {
            var company = await _companiesRepository.GetAsync(createEmployeeDto.CompanyId, userId);
            if (company == null)
            {
                throw new ValidationException(Messages.CompanyDoesNotExist, nameof(Messages.CompanyDoesNotExist));
            }

            var employeeUser = await _usersRepository.GetAsync(createEmployeeDto.UserUuid);
            if (employeeUser == null)
            {
                throw new ValidationException(Messages.EmployeeUserDoesNotExist, nameof(Messages.EmployeeUserDoesNotExist));
            }

            if (await _employeesRepository.GetAsync(filter: o => o.UserId == employeeUser.Id && o.CompanyId == createEmployeeDto.CompanyId) != null)
            {
                throw new ValidationException(Messages.UserEmployeeExistsForCompany, nameof(Messages.UserEmployeeExistsForCompany));
            }

            var employee = new Employee()
            {
                CompanyId = createEmployeeDto.CompanyId,
                Position = createEmployeeDto.Position,
                UserId = employeeUser.Id
            };

            if (await _userRolesRepository.GetAsync(filter: o => o.UserId == userId && o.RoleId == (int)Roles.Employee) == null)
            {
                var userRole = new UserRole()
                {
                    RoleId = (int)Roles.Employee,
                    UserId = employeeUser.Id
                };

                await _userRolesRepository.InsertAsync(userRole);
            }

            await _employeesRepository.InsertAsync(employee);

            return new CreateEmployeeResultDto();
        }

        public async Task<UpdateEmployeeResultDto> Update(UpdateEmployeeDto updateEmployeeDto, int userId)
        {
            var employee = await _employeesRepository.GetAsync(updateEmployeeDto.Id);
            if (employee == null)
            {
                throw new ValidationException(Messages.EmployeeDoesNotExist, nameof(Messages.EmployeeDoesNotExist));
            }

            var company = await _companiesRepository.GetAsync(employee.CompanyId, userId);
            if (company == null)
            {
                throw new ValidationException(Messages.EmployeeDoesNotExist, nameof(Messages.EmployeeDoesNotExist));
            }

            employee.Position = updateEmployeeDto.Position;

            await _employeesRepository.UpdateAsync(employee);

            return new UpdateEmployeeResultDto();
        }

        public async Task<DeleteEmployeeResultDto> Delete(int id, int userId)
        {
            var employee = await _employeesRepository.GetAsync(id);
            if (employee == null)
            {
                throw new ValidationException(Messages.EmployeeDoesNotExist, nameof(Messages.EmployeeDoesNotExist));
            }

            var company = await _companiesRepository.GetAsync(employee.CompanyId, userId);
            if (company == null)
            {
                throw new ValidationException(Messages.EmployeeDoesNotExist, nameof(Messages.EmployeeDoesNotExist));
            }

            await _employeesRepository.DeleteAsync(employee);

            return new DeleteEmployeeResultDto();
        }
    }
}
