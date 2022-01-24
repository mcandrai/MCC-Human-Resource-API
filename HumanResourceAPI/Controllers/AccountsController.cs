using HumanResourceAPI.Models;
using HumanResourceAPI.ModelView;
using HumanResourceAPI.Repository.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HumanResourceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : BaseController<Account, AccountRepository, string>
    {
        private readonly AccountRepository accountRepository;
        public IConfiguration _configuration;
        public AccountsController(AccountRepository accountRepository, IConfiguration configuration) : base(accountRepository)
        {
            this.accountRepository = accountRepository;
            this._configuration = configuration;
        }

        //api login employee
        [HttpPost]
        [Route("v1.0/login")]
        public ActionResult Login(Login login)
        {
            try
            {
                bool isEmail = accountRepository.GetEmployeeByEmail(login);

                if (!isEmail)
                {
                    return Ok(new JwtToken { status = HttpStatusCode.BadRequest, code = 0, idToken = null, message = "Account not found!" });
                }

                bool isLogin = accountRepository.Login(login);

                if (isLogin)
                {
                    string employeeRole = accountRepository.GetRoleEmployee(login.Email);
                    var claims = new List<Claim>
                    {
                        new Claim("email", login.Email),
                        new Claim("role", employeeRole)
                    };
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                            _configuration["Jwt:Issuer"],
                            _configuration["Jwt:Audience"],
                            claims,
                            expires: DateTime.UtcNow.AddMinutes(10),
                            signingCredentials: signIn
                        );
                    var idToken = new JwtSecurityTokenHandler().WriteToken(token);
                    claims.Add(new Claim("Token Security", idToken.ToString()));
                    return Ok(new JwtToken { status = HttpStatusCode.OK, code = 1, idToken = idToken, message = "Successful login!" });
                }
                else
                {
                    return Ok(new JwtToken { status = HttpStatusCode.BadRequest, code = 0, idToken = null, message = "Your password is invalid, Please try again!" });
                }

            }
            catch (Exception e)
            {
                return BadRequest(new JwtToken { status = HttpStatusCode.InternalServerError, idToken = null, message = "Something has gone wrong!" });
            }
        }


        [HttpPost]
        [Route("ForgotPassword")]

        public ActionResult ForgotPassword(ForgotPassword forgotPassword)
        {
            try
            {
                bool isEmail = accountRepository.GetAccountByEmail(forgotPassword);

                if (!isEmail)
                {
                    return BadRequest(new { status = HttpStatusCode.BadRequest, result = 0, message = "Account not found" });
                }
                else
                {
                    accountRepository.SendOTP(forgotPassword);

                    return Ok(new { status = HttpStatusCode.OK, result = 1, message = "OTP code sent to your email, Please check inbox or spam" });
                }
            }
            catch (Exception e)
            {

                return BadRequest(new { status = HttpStatusCode.InternalServerError, result = e, message = "Something has gone wrong" });
            }
        }

        [HttpPost]
        [Route("ChangePassword")]

        public ActionResult ChangePassword(ForgotPassword forgotPassword)
        {
            try
            {
                string password = forgotPassword.Password;

                if (String.IsNullOrEmpty(password))
                {
                    return BadRequest(new { status = HttpStatusCode.BadRequest, result = 0, message = "Password cannot be empty" });
                }

                bool isEmail = accountRepository.GetAccountByEmail(forgotPassword);

                if (!isEmail)
                {
                    return BadRequest(new { status = HttpStatusCode.BadRequest, result = 0, message = "Account not found" });
                }

                bool isValid = accountRepository.ExpiredOTP(forgotPassword);

                if (!isValid)
                {
                    return BadRequest(new { status = HttpStatusCode.BadRequest, result = 0, message = "OTP code has expired, Please request again" });
                }
                else
                {
                    bool isChange = accountRepository.ChangePassword(forgotPassword);

                    if (isChange)
                    {
                        return Ok(new { status = HttpStatusCode.OK, result = 1, message = "Success change password" });
                    }
                    else
                    {
                        return BadRequest(new { status = HttpStatusCode.BadRequest, result = 0, message = "OTP code does not match, Please try again" });
                    }

                }
            }
            catch (Exception e)
            {

                return BadRequest(new { status = HttpStatusCode.InternalServerError, result = e, message = "Something has gone wrong" });
            }
        }

        [HttpPost]
        [Route("GetRole")]

        public ActionResult GetRole()
        {
            try
            {
                var data = accountRepository.GetRoleEmployeeQuery();

                return Ok(data);
            }
            catch (Exception e)
            {
                return BadRequest(new { status = HttpStatusCode.InternalServerError, result = e, message = "Something has gone wrong" });

            }
        }
    }
}
