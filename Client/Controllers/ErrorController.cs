using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult NotFound()
        {
            return View();
        }

        public IActionResult Authorize()
        {
            return View();
        }

        public IActionResult Forbidden()
        {
            return View();
        }
    }
}
