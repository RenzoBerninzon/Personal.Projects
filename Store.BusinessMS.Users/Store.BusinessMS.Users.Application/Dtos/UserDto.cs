using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.BusinessMS.Users.Application.Dtos
{
    public class UserDto
    {
        public required string Id { get; set; }

        public required string Email { get; set; }

        public string? Name { get; set; }

        public string? LastName { get; set; }

        public string? MothersLastName { get; set; }

        public int DocTypeId { get; set; }

        public string? DocNumber { get; set; }

        public bool HasBoughtProducts { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
