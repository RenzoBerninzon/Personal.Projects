using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.BusinessMS.Users.Domain.Otp
{
    public class Otp
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public string? Code { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ExpirationDate { get; set; }
    }
}
