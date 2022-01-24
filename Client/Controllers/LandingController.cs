using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Controllers
{
    public class LandingController : Controller
    {
        [HttpGet("v1.0/landing")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
