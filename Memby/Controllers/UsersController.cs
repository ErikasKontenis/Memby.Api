using Memby.Application.User;
using Memby.Models;
using Microsoft.AspNetCore.Mvc;

namespace Memby.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        [HttpPost]
        public IActionResult Post(UpsertUserDto item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ModelStateResult(ModelState));
            }

            return Ok();
        }
    }
}
