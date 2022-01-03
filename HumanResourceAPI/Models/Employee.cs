using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HumanResourceAPI.Models
{
    [Table("tb_m_employees")]
    public class Employee
    {

        [Key]
        //[StringLength(16,ErrorMessage = "NIK harus 17 digit angka",MinimumLength =16)]
        public string NIK { get; set; }

        //[Required(ErrorMessage = "Nama Depan tidak boleh kosong")]
        public string FirstName { get; set; }

        //[Required(ErrorMessage = "Nama Belakang tidak boleh kosong")]
        public string LastName { get; set; }
        //public string FullName { get; set; }

        //[Required(ErrorMessage = "Nomor Handphone tidak boleh kosong")]
        public string Phone { get; set; }

        // [Required(ErrorMessage = "Tanggal Lahir tidak boleh kosong")]
        public DateTime BrithDate { get; set; }

        // [Required(ErrorMessage = "Email tidak boleh kosong")]
        public string Email { get; set; }

        //[Required(ErrorMessage = "Jenis Kelamin tidak boleh kosong")]
        public Gender Gender { get; set; }

        public virtual Account Account { get; set; }


    }
    public enum Gender
    {
        Male,
        Female
    }
}
