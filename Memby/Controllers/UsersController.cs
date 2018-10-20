using Memby.Application.Users;
using Memby.Contracts.Services;
using Memby.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Memby.Controllers
{
    [Authorize(Policy = "ApiUser")]
    [Route("api/[controller]")]
    public class UsersController : Controller
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
        [Route("Update")]
        public async Task<IActionResult> Update(UpdateUserInfoDto item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ModelStateResult(ModelState));
            }

            var userId = Convert.ToInt32(HttpContext.User.FindFirst(o => o.Type == "id").Value);
            var updateUserInfoResultDto = await _usersService.UpdateUserInfo(item, userId);

            return Ok(updateUserInfoResultDto);
        }
    }
}
