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

        ////Register employee
        //[HttpPost]
        //[Route("Register")]
        //public ActionResult Register(Register register)
        //{
        //    try
        //    {
        //        bool isDuplicateEmail = employeeRepository.DuplicateEmailValue(register);


        //        if (isDuplicateEmail)
        //        {
        //            return BadRequest(new { status = HttpStatusCode.BadRequest, result = 0, message = "Email already used!" });
        //        }

        //        bool isDuplicatePhone = employeeRepository.DuplicatePhoneValue(register);

        //        if (isDuplicatePhone)
        //        {
        //            return BadRequest(new { status = HttpStatusCode.BadRequest, result = 0, message = "Phone Number already used!" });
        //        }
        //        else
        //        {
        //            employeeRepository.RegisterStore(register);
        //            return Ok(new { status = HttpStatusCode.OK, result = 1, message = "Successfully added data!" });
        //        }

        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(new { status = HttpStatusCode.InternalServerError, result = e, message = "Something has gone wrong!" });
        //    }

        //}

        //Get all employee
        [HttpGet]
        [Route("Register")]
        public ActionResult<Register> GetRegisterData()
        {
            try
            {
                var result = employeeRepository.GetRegisterData();
                return Ok(new { status = HttpStatusCode.OK, result = 1, message = "Successfully get data", data = result });
            }
            catch (Exception e)
            {
                return BadRequest(new { status = HttpStatusCode.InternalServerError, result = e, message = "Something has gone wrong" });
            }
        }

        //Get employee by id
        [HttpPost]
        [Route("Register-NIK")]
        public ActionResult<EmployeeNIK> GetRegisterDataByNIK(EmployeeNIK employeeNIK)
        {
            try
            {
                var result = employeeRepository.GetRegisterDataByNIK(employeeNIK);
                if (result == null)
                {
                    return BadRequest(new { status = HttpStatusCode.BadRequest, result = 0, message = "NIK not available!" });
                }
                else
                {
                    return Ok(new { status = HttpStatusCode.OK, result = 1, message = "Successfully get data", data = result });
                }
                
            }
            catch (Exception e)
            {
                return BadRequest(new { status = HttpStatusCode.InternalServerError, result = e, message = "Something has gone wrong" });
            }
        }

        //Get detail  employee
        [HttpPost]
        [Route("Register-Detail")]
        public ActionResult<EmployeeNIK> GetRegisterDetailDataByNIK(EmployeeNIK employeeNIK)
        {
            try
            {
                var result = employeeRepository.GetRegisterDetailDataByNIK(employeeNIK);
                return Ok(new { status = HttpStatusCode.OK, result = 1, message = "Successfully get data", data = result });
            }
            catch (Exception e)
            {
                return BadRequest(new { status = HttpStatusCode.InternalServerError, result = e, message = "Something has gone wrong" });
            }
        }


        [HttpGet]
        [Route("Register-Client")]
        public ActionResult<Register> GetRegisterClient()
        {
            try
            {
                var result = employeeRepository.GetRegisterClient();
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(new { status = HttpStatusCode.InternalServerError, result = e, message = "Something has gone wrong" });
            }
        }

        //Get all employee based on university
        [HttpGet]
        [Route("Register/Base-University")]
        public ActionResult<Register> GetRegisterBaseUniversity()
        {
            try
            {
                var result = employeeRepository.GetRegisterBaseUniversity();
                return Ok(new { status = HttpStatusCode.OK, result = 1, message = "Successfully get data", data = result });
            }
            catch (Exception e)
            {
                return BadRequest(new { status = HttpStatusCode.InternalServerError, result = e, message = "Something has gone wrong" });
            }
        }

        //Get all employee based on gender
        [HttpGet]
        [Route("Register/Base-Gender")]
        public ActionResult<Register> GetRegisterBaseGender()
        {
            try
            {
                var result = employeeRepository.GetRegisterBaseGender();
                return Ok(new { status = HttpStatusCode.OK, result = 1, message = "Successfully get data", data = result });
            }
            catch (Exception e)
            {
                return BadRequest(new { status = HttpStatusCode.InternalServerError, result = e, message = "Something has gone wrong" });
            }
        }

        //Delete employee
        [HttpDelete]
        [Route("Register")]
        public ActionResult<Register> DeleteRegisterData(Register register)
        {
            try
            {
                bool result = employeeRepository.DeleteRegister(register);
                return Ok(new { status = HttpStatusCode.OK, result = 1, message = "Successfully delete data" });
            }
            catch (Exception e)
            {
                return BadRequest(new { status = HttpStatusCode.InternalServerError, result = e, message = "Something has gone wrong" });
            }
        }

        [HttpDelete]
        [Route("{NIK}")]
        public virtual ActionResult Remove(string NIK)
        {
            employeeRepository.RemoveEducation(NIK);
            var result = employeeRepository.RemoveEmployee(NIK);
            return Ok(result);
        }

        [HttpGet]
        [Route("Detail/{NIK}")]
        public virtual ActionResult<Register> Detail(string NIK)
        {
            var result = employeeRepository.DetailEmployee(NIK);
            return Ok(result);
        }

        [HttpPost]
        [Route("Register")]
        public ActionResult<Register> Register(Register register)
        {
            
        //    try
        //    {
        //        bool isDuplicateEmail = employeeRepository.DuplicateEmailValue(register);


        //        if (isDuplicateEmail)
        //        {
        //            return BadRequest(new { status = HttpStatusCode.BadRequest, result = 0, message = "Email already used!" });
        //        }

        //        bool isDuplicatePhone = employeeRepository.DuplicatePhoneValue(register);

        //        if (isDuplicatePhone)
        //        {
        //            return BadRequest(new { status = HttpStatusCode.BadRequest, result = 0, message = "Phone Number already used!" });
        //        }
        //        else
        //        {
        //            employeeRepository.RegisterStore(register);
        //            return Ok(new { status = HttpStatusCode.OK, result = 1, message = "Successfully added data!" });
        //        }

        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(new { status = HttpStatusCode.InternalServerError, result = e, message = "Something has gone wrong!" });
        //    }

            var result = employeeRepository.RegisterEmployee(register);
            return Ok(new { status = HttpStatusCode.OK, result = 1, message = "Successfully added data!" });

        }

        [HttpPut]
        [Route("Register/Update")]
        public ActionResult<Register> RegisterUpdate(Register register)
        {
            var result = employeeRepository.UpdateEmployee(register);
            return Ok(result);

        }

        //Get all employee based on gender
        [HttpGet]
        [Route("Register/Report")]
        public ActionResult<ReportData> ReportDataAll()
        {
            try
            {
                var result = employeeRepository.AllReport();
                return Ok(new { status = HttpStatusCode.OK, result = 1, message = "Successfully get data", data = result });
            }
            catch (Exception e)
            {
                return BadRequest(new { status = HttpStatusCode.InternalServerError, result = e, message = "Something has gone wrong" });
            }
        }
    }
}
