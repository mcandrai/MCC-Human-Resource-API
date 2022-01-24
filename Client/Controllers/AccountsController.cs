﻿using Client.Base;
using Client.Repositories.Data;
using HumanResourceAPI.Models;
using HumanResourceAPI.ModelView;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Controllers
{
    public class AccountsController : BaseController<Account, AccountRepository, string>
    {
        private readonly AccountRepository accountRepository;

        public AccountsController(AccountRepository repository) : base(repository)
        {
            accountRepository = repository;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Login(Login login)
        {
            var result = accountRepository.Login(login);
            return Json(result);
        }

        [HttpPost("v1.0/login")]
        public async Task<IActionResult> Auth(Login login)
        {
            var jwtToken = await accountRepository.Auth(login);
            var token = jwtToken.idToken;

            string status = jwtToken.status.ToString();

            if (token == null)
            {
                TempData["status"] = status;
                TempData["message"] = jwtToken.message;
                return RedirectToAction("index","login");
            }

            HttpContext.Session.SetString("JWToken", token);
      
                return RedirectToAction("index", "home");
            
           
        }

  
        [HttpGet("Accounts/Logout")]
        public IActionResult Logout()
        {
          

            HttpContext.Session.Clear();

            return RedirectToAction("index", "login");
        }

    }
}
