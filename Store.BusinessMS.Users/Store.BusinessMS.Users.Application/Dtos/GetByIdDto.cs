using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.BusinessMS.Users.Application.Dtos
{
    public class GetByIdDto
    {
        public string Id { get; set; } = null!;
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? MothersLastName { get; set; }
        public string? Email { get; set; }
    }
}
