using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechTestApi.Controllers
{
    public class BaseController : Controller
    {
        [Route("app/error")]
        public virtual IActionResult Error(int? statusCode = null, Exception ex = null)
        {
            #if DEBUG
            if (ex != null)
                return new JsonResult( new { ex = ex.Message });
            return BadRequest();
            #else
                return StatusCode("Status core : "(int)statusCode);
            #endif
        }
    }
}
