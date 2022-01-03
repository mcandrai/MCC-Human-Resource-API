using HumanResourceAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HumanResourceAPI.ModelView
{
    public class Register
    {
        public string NIK { get; set; }
        public string FirstName { get; set; }
        public string Lastname { get; set; }
        public string Phone { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public int Gender { get; set; }
        public string Password { get; set; }
        public int UniversityId { get; set; }
        public string UniversityName { get; set; }
        public string Degree { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal GPA { get; set; }
        public List<String> AccountRoles { get; set; }

    }
}
