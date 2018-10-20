using Memby.Application.Companies;
using System.Threading.Tasks;

namespace Memby.Contracts.Services
{
    public interface ICompaniesService
    {
        Task<CompanyDto> Get(int id, int userId);

        Task<CreateCompanyResultDto> Create(CreateCompanyDto createCompanyDto, int userId);

        Task<UpdateCompanyResultDto> Update(UpdateCompanyDto updateCompanyDto, int userId);

        Task<DeleteCompanyResultDto> Delete(int id, int userId);
    }
}
