﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Logs.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LogsController : Controller
    {
        public ActionResult<string> WriteMessage(string message)
        {
            return "WriteMessage !";
        }
    }
}
