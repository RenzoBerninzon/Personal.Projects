using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.BusinessMS.Users.Application.Command.UpdateUser.Request
{
    public class UpdateUserRequest
    {
        public string Id { get; set; } = null!;
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? MothersLastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Gender { get; set; }
    }
}
