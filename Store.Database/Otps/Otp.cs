using Store.Database.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Database.Domain.Otps
{
    [Table("OTP")]
    public class Otp
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
        [StringLength(20)]
        public string Code { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ExpirationDate { get; set; }
    }
}
