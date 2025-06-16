using Store.Database.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Database.Domain.Parameters
{
    public class Parameter
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(8)]
        public string Code { get; set; }


        [Required(AllowEmptyStrings = false)]
        public string Value { get; set; }

        [Required]
        public string Description { get; set; }

        public DateTime CreatedOn { set; get; }

        public DateTime UpdatedOn { set; get; }

        [ForeignKey("UpdatedByUserId")]
        public ApplicationUser UpdatedByUser { set; get; }

        public string UpdatedByUserId { set; get; }
    }
}
