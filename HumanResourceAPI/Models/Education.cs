using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HumanResourceAPI.Models
{
    [Table("tb_m_educations")]
    public class Education
    {
        public int EducationId { get; set; }
        public string Degree { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal GPA { get; set; }
        public virtual University University { get; set; }
        public int UniversityId { get; set; }
        public virtual ICollection<Profiling> Profilings { get; set; }
    }
}
