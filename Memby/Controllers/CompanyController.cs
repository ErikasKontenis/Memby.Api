using Memby.Application.Companies;
using Memby.Contracts.Services;
using Memby.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Memby.WebApi.Controllers
{
    [Authorize(Policy = "LegalPerson")]
    [Route("api/[controller]")]
    public class CompanyController : ApiControllerBase
    {
        private readonly ICompaniesService _companiesService;

        public CompanyController(ICompaniesService companiesService)
        {
            _companiesService = companiesService;
        }

        [HttpGet]
        [Route("Get/{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ModelStateResult(ModelState));
            }

            var companyDto = await _companiesService.Get(id, UserId);

            return Ok(companyDto);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("Create")]
        public async Task<IActionResult> Create(CreateCompanyDto item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ModelStateResult(ModelState));
            }

            var createCompanyResultDto = await _companiesService.Create(item, UserId);

            return Ok(createCompanyResultDto);
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update(UpdateCompanyDto item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ModelStateResult(ModelState));
            }

            var updateCompanyResultDto = await _companiesService.Update(item, UserId);

            return Ok(updateCompanyResultDto);
        }

        [HttpDelete]
        [Route("Delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ModelStateResult(ModelState));
            }

            var deleteCompanyResultDto = await _companiesService.Delete(id, UserId);

            return Ok(deleteCompanyResultDto);
        }
    }
}
