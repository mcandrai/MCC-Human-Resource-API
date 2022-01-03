using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HumanResourceAPI.Models
{
    [Table("tb_t_profiling")]
    public class Profiling
    {
        [Key]
        public string NIK { get; set; }
        public virtual Education Education { get; set; }
        public virtual Account Account { get; set; }
        public int EducationId { get; set; }

    }
}
