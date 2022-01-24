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
            int countEmployee = myContext.Employees.ToList().Count;
            string result;
            if (countEmployee < 1)
            {
                return result = nowYear + "0" + (countEmployee + 1).ToString();
            }
            else
            {
                var maxId = myContext.Employees.Max(e => e.NIK);
                int setId = Int32.Parse(maxId) + 1;
                return result = setId.ToString();
            }
            
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

        
        public int RegisterEmployee(Register register)
        {
         
            var employee = new Employee
            {
                NIK = GenerateNIK(),
                FirstName = register.FirstName,
                LastName = register.LastName,
                Phone = register.Phone,
                BrithDate = register.BrithDate,
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

        public int UpdateEmployee( Register register)
        {
            var employee = new Employee
            {
                NIK = register.NIK,
                FirstName = register.FirstName,
                LastName = register.LastName,
                Phone = register.Phone,
                BrithDate = register.BrithDate,
                Email = register.Email,
                Gender = (Models.Gender)register.Gender
            };

            myContext.Entry(employee).State = EntityState.Modified;
            myContext.SaveChanges();

            var profiling = myContext.Profilings.Where(p => p.NIK == register.NIK).FirstOrDefault();

            var education = new Education
            {
                EducationId = profiling.EducationId,
                Degree = register.Degree,
                GPA = register.GPA,
                UniversityId = register.UniversityId
            };
            myContext.Entry(education).State = EntityState.Modified;
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
                           Nik = emp.NIK,
                           FullName = emp.FirstName + " " + emp.LastName,
                           Phone = emp.Phone,
                           BrithDate = emp.BrithDate.ToString("dd MMMM yyyy"),
                           Email = emp.Email,
                           Degree = edu.Degree,
                           GPA = edu.GPA,
                           UniversityName = uni.UniversityName,
                           AccountRole = myContext.AccountRoles.Where(a => a.NIK == emp.NIK).Select(ar => ar.Role.RoleName).ToList()
                        };

            return list;
        }


        public IEnumerable<Object> GetRegisterClient()
        {

            var list = from emp in myContext.Employees
                       join acc in myContext.Accounts on emp.NIK equals acc.NIK
                       join pro in myContext.Profilings on acc.NIK equals pro.NIK
                       join edu in myContext.Educations on pro.EducationId equals edu.EducationId
                       join uni in myContext.Universities on edu.UniversityId equals uni.UniversityId
                       select new
                       {
                           Nik = emp.NIK,
                           FirstName = emp.FirstName,
                           LastName = emp.LastName,
                           Phone = emp.Phone,
                           BrithDate = emp.BrithDate,
                           Email = emp.Email,
                           Degree = edu.Degree,
                           GPA = edu.GPA,
                           UniversityName = uni.UniversityName,
                       };

            return list;
        }

        public Employee GetRegisterDataByNIK(EmployeeNIK employeeNIK)
        {
            var data =  myContext.Employees.Where(e => e.NIK == employeeNIK.NIK).FirstOrDefault();
            if (data == null)
            {
                return null;
            }
            data.BrithDate.ToString("yyyy-mm-dd");
            return data;
        }

        public IEnumerable<Object> GetRegisterDetailDataByNIK(EmployeeNIK employeeNIK)
        {
     
            var list = from emp in myContext.Employees
                       join acc in myContext.Accounts on emp.NIK equals acc.NIK
                       join pro in myContext.Profilings on acc.NIK equals pro.NIK
                       join edu in myContext.Educations on pro.EducationId equals edu.EducationId
                       join uni in myContext.Universities on edu.UniversityId equals uni.UniversityId
                       where emp.NIK == employeeNIK.NIK
                       select new
                       {
                           Nik = emp.NIK,
                           FullName = emp.FirstName + " " + emp.LastName,
                           Phone = emp.Phone,
                           BirthDate = emp.BrithDate.ToString("dd MMMM yyyy"),
                           Email = emp.Email,
                           Degree = edu.Degree,
                           GPA = edu.GPA,
                           UniversityId = uni.UniversityId,
                           UniversityName = uni.UniversityName,
                           AccountRole = myContext.AccountRoles.Where(a => a.NIK == emp.NIK).Select(ar => ar.Role.RoleName).ToList()
                       };

            return list;
        }


        public IEnumerable<BaseRegisterUniversity> GetRegisterBaseUniversity()
        {
            var list = from edu in myContext.Educations
                       join uni in myContext.Universities on edu.UniversityId equals uni.UniversityId
                       group uni by new {edu.UniversityId, uni.UniversityName } into Group
                       select new BaseRegisterUniversity
                       {
                           UniversityId = Group.Key.UniversityId,
                           UniversityName = Group.Key.UniversityName,
                           BaseUniversity = Group.Count()
                       };

            return list.ToList();
        }

        public IEnumerable<BaseRegisterGender> GetRegisterBaseGender()
        {
            var list = from emp in myContext.Employees
                       group emp by new { emp.Gender } into Group
                       select new BaseRegisterGender
                        {
                            Gender = (int)Group.Key.Gender,
                            NameGender = (int)Group.Key.Gender == 1? "Male":"Female",
                            BaseGender = Group.Count()
                        };

            return list.ToList();
        }

        public bool DeleteRegister(Register register)
        {
            myContext.Remove(myContext.Employees.Single(e => e.NIK == register.NIK));
            myContext.SaveChanges();
            return true;
        }

        public bool RemoveEducation(string NIK)
        {
            var profilings = myContext.Profilings.Find(NIK);
            var education = myContext.Educations.Where(ed => ed.EducationId == profilings.EducationId).FirstOrDefault();
            if (education == null)
            {
                throw new ArgumentNullException("entity");
            }
            myContext.Educations.Remove(education);
            myContext.SaveChanges();
            return true;
        }


        public bool RemoveEmployee(string NIK)
        {
            var employees = myContext.Employees.Find(NIK);
            if (employees == null)
            {
                throw new ArgumentNullException("entity");
            }
            myContext.Employees.Remove(employees);
            myContext.SaveChanges();
            return true;
        }

        public Register DetailEmployee(string NIK)
        {

            var query = myContext.Employees.Where(e => e.NIK == NIK).Include(a => a.Account).ThenInclude(p => p.Profiling).ThenInclude(e => e.Education).ThenInclude(u => u.University).FirstOrDefault();
            if (query == null)
            {
                return null;
            }
            var getData = new Register
            {
                NIK = query.NIK,
                FirstName = query.FirstName,
                LastName = query.LastName,
                Phone = query.Phone,
                BrithDate = query.BrithDate,
                Email = query.Email,
                Gender = (int)query.Gender,
                Degree = query.Account.Profiling.Education.Degree,
                GPA = query.Account.Profiling.Education.GPA,
                UniversityId = query.Account.Profiling.Education.UniversityId,
                UniversityName = query.Account.Profiling.Education.University.UniversityName,
                AccountRoles = myContext.AccountRoles.Where(accountrole => accountrole.NIK == query.NIK).Select(accountrole => accountrole.Role.RoleName).ToList()
            };

            return getData;

        }

        public ReportData AllReport()
        {
            var emp = myContext.Employees.Count();
            var acc = myContext.Accounts.Count();
            var edu = myContext.Educations.Count();
            var uni = myContext.Universities.Count();

            var getReport = new ReportData
            {
                employee = emp,
                account = acc,
                education = edu,
                university = uni
            };

            return getReport;
        }
    }
}
