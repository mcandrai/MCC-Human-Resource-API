using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HumanResourceAPI.Models
{
    [Table("tb_m_universities")]
    public class University
    {
        
        public int UniversityId { get; set; }
        public string UniversityName { get; set; }
        
        public virtual ICollection<Education> Educations { get; set; }
    }
}
