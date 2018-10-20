using Memby.Application.Companies;
using Memby.Contracts.Repositories;
using Memby.Contracts.Services;
using Memby.Core.Enums;
using Memby.Core.Exceptions;
using Memby.Core.Resources;
using Memby.Domain.Companies;
using Memby.Domain.Users;
using System.Threading.Tasks;

namespace Memby.Services.Companies
{
    public class CompaniesService : ICompaniesService
    {
        private readonly ICompaniesRepository _companiesRepository;
        private readonly IUserRolesRepository _userRolesRepository;

        public CompaniesService(ICompaniesRepository companiesRepository,
            IUserRolesRepository userRolesRepository)
        {
            _companiesRepository = companiesRepository;
            _userRolesRepository = userRolesRepository;
        }

        public async Task<CompanyDto> Get(int id, int userId)
        {
            var company = await _companiesRepository.GetAsync(id, userId);
            if (company == null)
            {
                throw new ValidationException(Messages.CompanyDoesNotExist, nameof(Messages.CompanyDoesNotExist));
            }

            var companyDto = new CompanyDto()
            {
                Address = company.Address,
                BrandName = company.BrandName,
                Logo = company.Logo,
                Name = company.Name,
                RegistrationNumber = company.RegistrationNumber,
                VatNumber = company.VatNumber
            };

            return companyDto;
        }

        public async Task<CreateCompanyResultDto> Create(CreateCompanyDto createCompanyDto, int userId)
        {
            var company = new Company();
            company.Address = createCompanyDto.Address;
            company.BrandName = createCompanyDto.BrandName;
            company.Logo = createCompanyDto.Logo;
            company.Name = createCompanyDto.Name;
            company.RegistrationNumber = createCompanyDto.RegistrationNumber;
            company.UserId = userId;
            company.VatNumber = createCompanyDto.VatNumber;

            if (await _userRolesRepository.GetAsync(filter: o => o.UserId == userId && o.RoleId == (int)Roles.LegalPerson) == null)
            {
                var userRole = new UserRole()
                {
                    RoleId = (int)Roles.LegalPerson,
                    UserId = userId
                };

                await _userRolesRepository.InsertAsync(userRole);
            }

            return new CreateCompanyResultDto()
            {
                CompanyId = (await _companiesRepository.InsertAsync(company)).Id
            };
        }

        public async Task<UpdateCompanyResultDto> Update(UpdateCompanyDto updateCompanyDto, int userId)
        {
            var company = await _companiesRepository.GetAsync(updateCompanyDto.Id, userId);
            if (company == null)
            {
                throw new ValidationException(Messages.CompanyDoesNotExist, nameof(Messages.CompanyDoesNotExist));
            }

            company.Address = updateCompanyDto.Address;
            company.BrandName = updateCompanyDto.BrandName;
            company.Logo = updateCompanyDto.Logo;
            company.Name = updateCompanyDto.Name;
            company.RegistrationNumber = updateCompanyDto.RegistrationNumber;
            company.VatNumber = updateCompanyDto.VatNumber;

            await _companiesRepository.UpdateAsync(company);

            return new UpdateCompanyResultDto();
        }

        public async Task<DeleteCompanyResultDto> Delete(int id, int userId)
        {
            var company = await _companiesRepository.GetAsync(id, userId);
            if (company == null)
            {
                throw new ValidationException(Messages.CompanyDoesNotExist, nameof(Messages.CompanyDoesNotExist));
            }

            await _companiesRepository.DeleteAsync(company);

            return new DeleteCompanyResultDto();
        }
    }
}
