using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HumanResourceAPI.ModelView
{
    public class ForgotPassword
    {
        public string Email { get; set; }
        public int OTP { get; set; }
        public string Password { get; set; }
        public DateTime ExpiredOTP { get; set; }
        public bool IsUse { get; set; }
    }
}
