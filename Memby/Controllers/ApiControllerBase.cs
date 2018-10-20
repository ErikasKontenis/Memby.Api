using Microsoft.AspNetCore.Mvc;
using System;

namespace Memby.WebApi.Controllers
{
    public class ApiControllerBase : Controller
    {
        protected int UserId => Convert.ToInt32(HttpContext?.User?.FindFirst(o => o.Type == "Id")?.Value ?? "0");
    }
}
