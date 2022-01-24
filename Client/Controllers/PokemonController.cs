using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Controllers
{
    [Authorize(Roles = "Employee, Manager, Direktur")]
    public class PokemonController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
