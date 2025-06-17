using Microsoft.EntityFrameworkCore;
using Store.BusinessMS.Users.Application.Interfaces.Repositories;
using Store.BusinessMS.Users.Domain.User;
using Store.BusinessMS.Users.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.BusinessMS.Users.Infrastructure.Repositories
{
    public class ApplicationUserRepository : GenericRepository<ApplicationUser, StoreDbContext>, IGenericRepository<ApplicationUser>
    {
        public ApplicationUserRepository(StoreDbContext context) : base(context) { }
    }
}
