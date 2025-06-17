using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.BusinessMS.Users.Application.Command.ChangePassword.Request
{
    public class ChangePasswordRequest
    {
        public string UserId { get; set; }
        public string Otp { get; set; }
        public string NewPassword { get; set; }
    }
}
