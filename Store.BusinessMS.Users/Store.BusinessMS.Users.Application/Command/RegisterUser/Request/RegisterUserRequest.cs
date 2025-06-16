using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.BusinessMS.Users.Application.Command.RegisterUser.Request
{
    public class RegisterUserRequest
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? MothersLastName { get; set; }
        public int DocTypeId { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Gender { get; set; }
        public string? DocNumber { get; set; }
        public string? UtmSource { get; set; }
        public string? UtmMedium { get; set; }
        public string? UtmCampaign { get; set; }
        public string? UtmTerm { get; set; }
        public string? UtmContent { get; set; }
    }
}
