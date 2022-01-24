using HumanResourceAPI.Context;
using HumanResourceAPI.Models;
using HumanResourceAPI.ModelView;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace HumanResourceAPI.Repository.Data
{
    public class AccountRepository : GeneralRepository<MyContext, Account, string>
    {
        private readonly MyContext myContext;
        public AccountRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }

        //validate data employee login
        public bool Login(Login login)
        {
            var getEmail = myContext.Employees.FirstOrDefault(e => e.Email == login.Email);

            var getAccount = myContext.Accounts.FirstOrDefault(a => a.NIK == getEmail.NIK);

            if (getAccount == null || getEmail == null)
            {
                return GetEmployeeByEmail(login);
            }
            else if (BCrypt.Net.BCrypt.Verify(login.Password, getAccount.Password))
            {
                return true;
            }

            return false;
        }

        public bool ChangePassword(ForgotPassword forgotPassword)
        {
            var getEmail = myContext.Employees.FirstOrDefault(e => e.Email == forgotPassword.Email);

            var getAccount = myContext.Accounts.FirstOrDefault(e => e.NIK == getEmail.NIK);

            int employeeOTP = getAccount.OTP;

            int employeeSetOTP = forgotPassword.OTP;

            bool isActiveOTP = getAccount.IsUse;

            if (employeeOTP != employeeSetOTP || isActiveOTP == false )
            {
                return false;
            }

            string password = BCrypt.Net.BCrypt.HashPassword(forgotPassword.Password);
            var account = new Account()
            {
                NIK = getAccount.NIK,
                Password = password,
                OTP = 0,
                IsUse = false,
                ExpiredOTP = getAccount.ExpiredOTP
            };
            myContext.Entry(getAccount).State = EntityState.Detached;
            myContext.Entry(account).State = EntityState.Modified;
            myContext.SaveChanges();

            return true;
        }

        public bool GetEmployeeByEmail(Login login)
        {
            var getEmail = myContext.Employees.FirstOrDefault(e => e.Email == login.Email);

            if (getEmail == null)
            {
                return false;
            }
            return true;
        }

        public bool GetAccountByEmail(ForgotPassword forgotPassword)
        {
            var getEmail = myContext.Employees.FirstOrDefault(e => e.Email == forgotPassword.Email);

            if (getEmail == null)
            {
                return false;
            }

            return true;
        }

        public int GenerateOTP()
        {
            Random rand = new Random();
            int randomOTP = rand.Next(100000, 999999);
            return randomOTP;
        }

        public bool SendOTP(ForgotPassword forgotPassword)
        {
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential("mccreg61net@gmail.com", "61mccregnet"),
                EnableSsl = true
            };

            DateTime now = DateTime.Now;
            DateTime expiredToken = now.AddMinutes(5);

            int randomOTP = GenerateOTP();
            string bodyMessage = $"To reset your password, use code OTP : {randomOTP} \n\n Expired : {expiredToken} .";
            client.Send("mccreg61net@gmail.com", forgotPassword.Email, "HR - RESET PASSWORD", bodyMessage);

            SetOTP(forgotPassword.Email, randomOTP);

            return true;
        }

        public void SetOTP(string email, int randomOTP)
        {

            var getEmail = myContext.Employees.Where(e => e.Email == email).FirstOrDefault();

            DateTime now = DateTime.Now;
            DateTime addExpiredToken = now.AddMinutes(5);

            var getAccount = myContext.Accounts.Where(a => a.NIK == getEmail.NIK).FirstOrDefault();
            var account = new Account()
            {
                NIK = getAccount.NIK,
                Password = getAccount.Password,
                OTP = randomOTP,
                IsUse = true,
                ExpiredOTP = addExpiredToken
            };
            myContext.Entry(getAccount).State = EntityState.Detached;
            myContext.Entry(account).State = EntityState.Modified;
            myContext.SaveChanges();
        }

        public bool ExpiredOTP(ForgotPassword forgotPassword)
        {
            var getEmail = myContext.Employees.FirstOrDefault(e => e.Email == forgotPassword.Email);

            var getAccount = myContext.Accounts.FirstOrDefault(e => e.NIK == getEmail.NIK);

            DateTime otp = getAccount.ExpiredOTP;

            DateTime now = DateTime.Now;

            int compareDate = DateTime.Compare(now, otp);

            if (compareDate > 0)
            {
                return false;
            }
            return true;
        }

        public string GetRoleEmployee(string email)
        {
            var getEmail = myContext.Employees.FirstOrDefault(e => e.Email == email);

            var result = myContext.AccountRoles.Where(a => a.NIK == getEmail.NIK).Select(ar => ar.Role.RoleName).ToList();

            return string.Join(",", result);
        }

        public IEnumerable<Object> GetRoleEmployeeQuery()
        {
            var getEmail = myContext.Employees.FirstOrDefault(e => e.Email == "672017115@student.uksw.edu");

            var data = from rol in myContext.Roles
                       join acc_rol in myContext.AccountRoles on rol.RoleId equals acc_rol.RoleId
                       join emp in myContext.Accounts on acc_rol.NIK equals emp.NIK
                       where (acc_rol.NIK == getEmail.NIK)
                       select new
                       {
                           Roles = rol.RoleName
                       };
            return data;
        }

    }
}
