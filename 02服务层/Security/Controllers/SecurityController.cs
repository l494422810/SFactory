using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Security.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SecurityController : Controller
    {
        private IAntiforgery antiforgery;
        public SecurityController(IAntiforgery antiforgery)
        {
            this.antiforgery = antiforgery;
        }

      
        public ActionResult GetXsrfToken()
        {
            return Ok();
        }
    }

}
