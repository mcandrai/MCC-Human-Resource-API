using Client.Base;
using Client.Repositories.Data;
using HumanResourceAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Controllers
{
    public class UniversitiesController : BaseController<University, UniversityRepository, string>
    {

        private readonly UniversityRepository universityRepository;
        public UniversitiesController(UniversityRepository repository) : base(repository)
        {
            universityRepository = repository;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
