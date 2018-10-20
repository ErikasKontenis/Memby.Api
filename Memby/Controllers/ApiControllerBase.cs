using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Memby.WebApi.Controllers
{
    public class ApiControllerBase : Controller
    {
        protected int UserId => Convert.ToInt32(HttpContext?.User?.FindFirst(o => o.Type == "id")?.Value ?? "0");
    }
}
