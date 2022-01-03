using HumanResourceAPI.Context;
using HumanResourceAPI.Models;
using HumanResourceAPI.ModelView;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HumanResourceAPI.Repository.Data
{
    public class EmployeeRepository : GeneralRepository<MyContext, Employee, string>
    {
        private readonly MyContext myContext;


        public EmployeeRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }

        public string GenerateNIK()
        {
            string nowYear = DateTime.Now.Year.ToString();
            int countEmployee = myContext.Employees.ToList().Count + 1;
            string result = nowYear + "0" + countEmployee.ToString();
            return result;

        }

        public bool DuplicateEmailValue(Register register)
        {
            int getEmail = myContext.Employees.Where(e => e.Email == register.Email).Count();
            if (getEmail > 0)
            {
                return true;
            }
            return false;
        }

        public bool DuplicatePhoneValue(Register register)
        {
            int getPhone = myContext.Employees.Where(e => e.Phone == register.Phone).Count();
            if (getPhone > 0)
            {
                return true;
            }
            return false;
        }

        public int RegisterStore(Register register)
        {

            var employee = new Employee
            {
                NIK = GenerateNIK(),
                FirstName = register.FirstName,
                LastName = register.Lastname,
                Phone = register.Phone,
                BrithDate = register.BirthDate,
                Email = register.Email,
                Gender = (Models.Gender)register.Gender
            };

            myContext.Employees.Add(employee);
            myContext.SaveChanges();

            string hashPassword = BCrypt.Net.BCrypt.HashPassword(register.Password);
            var account = new Account
            {
                NIK = employee.NIK,
                Password = hashPassword,
            };
            myContext.Accounts.Add(account);
            myContext.SaveChanges();

            var accountrole = new AccountRole
            {
                NIK = employee.NIK,
                RoleId = 3
            };

            myContext.AccountRoles.Add(accountrole);
            myContext.SaveChanges();

            var education = new Education
            {
                Degree = register.Degree,
                GPA = register.GPA,
                UniversityId = register.UniversityId
            };
            myContext.Educations.Add(education);
            myContext.SaveChanges();

            var profiling = new Profiling
            {
                NIK = account.NIK,
                EducationId = education.EducationId
            };
            myContext.Profilings.Add(profiling);
            return myContext.SaveChanges();


        }

        public IEnumerable<Object> GetRegisterData()
        {


            var list = from emp in myContext.Employees
                       join acc in myContext.Accounts on emp.NIK equals acc.NIK
                       join pro in myContext.Profilings on acc.NIK equals pro.NIK
                       join edu in myContext.Educations on pro.EducationId equals edu.EducationId
                       join uni in myContext.Universities on edu.UniversityId equals uni.UniversityId
                       select new
                       {
                           FullName = emp.FirstName + " " + emp.LastName,
                           Phone = emp.Phone,
                           BirthDate = emp.BrithDate,
                           Email = emp.Email,
                           Degree = edu.Degree,
                           GPA = edu.GPA,
                           UniversityName = uni.UniversityName,
                           AccountRole = myContext.AccountRoles.Where(a => a.NIK == emp.NIK).Select(ar => ar.Role.RoleName).ToList()
                        };

            return list;
        }
    }
}
