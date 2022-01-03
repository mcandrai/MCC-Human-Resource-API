using HumanResourceAPI.Models;
using HumanResourceAPI.ModelView;
using HumanResourceAPI.Repository.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace HumanResourceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : BaseController<Employee, EmployeeRepository, string>
    {
        private readonly EmployeeRepository employeeRepository;
        
        public EmployeesController(EmployeeRepository employeeRepository) : base(employeeRepository)
        {
            this.employeeRepository = employeeRepository;
            
        }

        [HttpPost]
        [Route("Register")]

        public ActionResult Register(Register register)
        {
            try
            {
                bool isDuplicateEmail = employeeRepository.DuplicateEmailValue(register);
                

                if (isDuplicateEmail)
                {
                    return BadRequest(new { status = HttpStatusCode.BadRequest, result = 0, message = "Email Number already used" });
                }

                bool isDuplicatePhone = employeeRepository.DuplicatePhoneValue(register);
                
                if (isDuplicatePhone)
                {
                    return BadRequest(new { status = HttpStatusCode.BadRequest, result = 0, message = "Phone Number already used" });
                }
                else
                {
                    employeeRepository.RegisterStore(register);
                    return Ok(new { status = HttpStatusCode.OK, result = 1, message = "Successfully added data" });
                }
            
                
            }
            catch (Exception e)
            {
                return BadRequest(new { status = HttpStatusCode.InternalServerError, result = e, message = "Something has gone wrong" });
            }

        }

        //[Authorize(Roles ="Manager")]
      
      [HttpGet]
      [Route("Register")]
       // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

        public ActionResult<Register> GetRegisterData()
        {
            var result = employeeRepository.GetRegisterData();
            return Ok(result);
        }

       [HttpGet]
       [Route("TestCors")]
       public ActionResult TestCors()
        {
            return Ok("Test Cors berhasil");
        }


    }
}
