using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HumanResourceAPI.Models
{
    [Table("tb_t_account")]
    public class Account
    {
        [Key]
        //[StringLength(16, ErrorMessage = "NIK harus 17 digit angka", MinimumLength = 16)]
        public string NIK { get; set; }
        public string Password { get; set; }
        public int OTP { get; set; }
        public DateTime ExpiredOTP { get; set; }
        public bool IsUse { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Profiling Profiling { get; set; }
        public ICollection<AccountRole> AccountRoles { get; set; }
      
    }
}
