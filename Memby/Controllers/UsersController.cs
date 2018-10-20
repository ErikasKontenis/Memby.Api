using Memby.Application.Users;
using Memby.Contracts.Services;
using Memby.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Memby.WebApi.Controllers
{
    [Authorize(Policy = "ApiUser")]
    [Route("api/[controller]")]
    public class UsersController : ApiControllerBase
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("Register")]
        public async Task<IActionResult> Register(CreateUserDto item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ModelStateResult(ModelState));
            }

            var createUserResultDto = await _usersService.Register(item);

            return Ok(createUserResultDto);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginUserDto item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ModelStateResult(ModelState));
            }

            var loginUserResultDto = await _usersService.Login(item);

            return Ok(loginUserResultDto);
        }

        [HttpPut]
        [Route("UpdateUserInfo")]
        public async Task<IActionResult> UpdateUserInfo(UpdateUserInfoDto item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ModelStateResult(ModelState));
            }

            var updateUserInfoResultDto = await _usersService.UpdateUserInfo(item, UserId);

            return Ok(updateUserInfoResultDto);
        }

        [HttpPut]
        [Route("UpdateUserEmail")]
        public async Task<IActionResult> UpdateUserEmail(UpdateUserEmailDto item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ModelStateResult(ModelState));
            }

            var updateUserEmailResultDto = await _usersService.UpdateUserEmail(item, UserId);

            return Ok(updateUserEmailResultDto);
        }
    }
}
