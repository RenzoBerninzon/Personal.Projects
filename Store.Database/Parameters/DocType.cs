using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Database.Domain.Parameters
{
    public class DocType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string DocTypeDescription { get; set; }

        public static readonly int DNI = 1;
        public static readonly int CarnetExtranjeria = 2;
        public static readonly int Pasaporte = 3;
        public static readonly int RUC = 4;
        public static readonly int PTP = 5;
    }
}
