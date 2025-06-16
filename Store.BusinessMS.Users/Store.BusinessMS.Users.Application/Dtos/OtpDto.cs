using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.BusinessMS.Users.Application.Dtos
{
    public class OtpDto
    {
        public string? UserId { get; set; }
        public string? Email { get; set; }
        public DateTime? ExpirationDate { get; set; }
    }
}
