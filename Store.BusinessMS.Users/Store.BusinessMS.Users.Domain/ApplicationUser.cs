using System.ComponentModel.DataAnnotations.Schema;

namespace Store.BusinessMS.Users.Domain
{
    public partial class ApplicationUser
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string MothersLastName { get; set; }
        public string DocNumber { get; set; }
        public string Gender { get; set; }
        public int DocTypeId { get; set; }
        public int CustomerId { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public bool HasBoughtProducts { get; set; }
        public string UtmSource { get; set; }
        public string UtmMedium { get; set; }
        public string UtmCampaign { get; set; }
        public string UtmTerm { get; set; }
        public string UtmContent { get; set; }
    }
}
