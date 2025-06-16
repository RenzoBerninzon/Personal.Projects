using Microsoft.EntityFrameworkCore;
using Store.Database.Domain.Parameters;
using Store.Database.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Database.Domain.Customers
{
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTime BirthDate { get; set; }

        public DocType DocType { get; set; }

        public int DocTypeId { get; set; }

        public string DocNumber { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string MothersLastName { get; set; }

        public string Address { get; set; }

        public string State { get; set; }

        public int CountryId { get; set; }

        [ForeignKey("CountryId")]
        public Country Country { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime CreatedOn { set; get; }

        public DateTime UpdatedOn { set; get; }

        public string Email { get; set; }

        public bool ShouldGenerateInvoice { get; set; }
    }
}
