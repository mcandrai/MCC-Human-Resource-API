using Client.Base;
using Client.Repositories.Data;
using HumanResourceAPI.Models;
using HumanResourceAPI.ModelView;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Controllers
{
    [Authorize(Roles ="Direktur, Manager")]
    public class EmployeesController : BaseController<Employee, EmployeeRepository, string>
    {
        private readonly EmployeeRepository employeeRepository;
        public EmployeesController(EmployeeRepository repository) : base(repository)
        {
            employeeRepository = repository;
        }


        public IActionResult Master()
        {
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Chart()
        {
            return View();
        }

        [HttpDelete("Employees/Remove/{NIK}")]
        public JsonResult Remove(string NIK)
        {
            var result = employeeRepository.Remove(NIK);
            return Json(result);
        }

        [HttpGet("Employees/Detail/{NIK}")]
        public async Task<JsonResult> Detail(string NIK)
        {
            var result = await employeeRepository.Detail(NIK);
            return Json(result);
        }

        [HttpGet("Employees/Report")]
        public JsonResult ReportAll()
        {
            var result =  employeeRepository.ReportAll();
            return Json(result);
        }


        [HttpPost]
        public JsonResult Register(Register register)
        {
            var result = employeeRepository.Register(register);
            return Json(result);
        }


        [HttpPut]
        public JsonResult Update(Register register)
        {
            var result = employeeRepository.Update(register);
            return Json(result);
        }

        [HttpPost]
        public JsonResult Login(Register register)
        {
            var result = employeeRepository.Register(register);
            return Json(result);
        }


    }
}
