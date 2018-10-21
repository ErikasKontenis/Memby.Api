using Memby.Application.Employees;
using Memby.Contracts.Services;
using Memby.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Memby.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class EmployeeController : ApiControllerBase
    {
        private readonly IEmployeesService _employeesService;

        public EmployeeController(IEmployeesService employeesService)
        {
            _employeesService = employeesService;
        }

        [HttpGet]
        [Authorize(Policy = "CompanyOwner")]
        [Route("Get/{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ModelStateResult(ModelState));
            }

            var employeeDto = await _employeesService.Get(id, UserId);

            return Ok(employeeDto);
        }

        [HttpPost]
        [Authorize(Policy = "CompanyOwner")]
        [Route("Create")]
        public async Task<IActionResult> Create(CreateEmployeeDto item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ModelStateResult(ModelState));
            }

            var createEmployeeResultDto = await _employeesService.Create(item, UserId);

            return Ok(createEmployeeResultDto);
        }

        [HttpPut]
        [Authorize(Policy = "CompanyOwner")]
        [Route("Update")]
        public async Task<IActionResult> Update(UpdateEmployeeDto item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ModelStateResult(ModelState));
            }

            var updateEmployeeResultDto = await _employeesService.Update(item, UserId);

            return Ok(updateEmployeeResultDto);
        }

        [HttpDelete]
        [Authorize(Policy = "CompanyOwner")]
        [Route("Delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ModelStateResult(ModelState));
            }

            var deleteEmployeeResultDto = await _employeesService.Delete(id, UserId);

            return Ok(deleteEmployeeResultDto);
        }
    }
}
