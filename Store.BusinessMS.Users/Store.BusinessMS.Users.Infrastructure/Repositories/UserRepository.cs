using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Store.BusinessMS.Users.Domain.User;
using Store.BusinessMS.Users.Infrastructure.Database;
using Store.BusinessMS.Users.Application.Interfaces.Repositories;
using Store.BusinessMS.Users.Application.Core;

namespace Store.BusinessMS.Users.Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<ApplicationUser, StoreDbContext>, IUserRepository
    {
        private readonly DbSet<ApplicationUser> _aspNetUsers;
        private readonly ILogger _logger;

        public UserRepository(ILoggerFactory logger, StoreDbContext context) : base(context)
        {
            _aspNetUsers = _context.Set<ApplicationUser>();
            _logger = logger.CreateLogger<UserRepository>();
        }

        public async Task<PagedList<ApplicationUser>> GetAllUsers(int pageNumber, int pageSize)
        {
            var query = _aspNetUsers.AsNoTracking();

            var total = await query.CountAsync();

            var items = await query
                .OrderBy(u => u.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedList<ApplicationUser>(items, pageNumber, pageSize, total);
        }

        public async Task<ApplicationUser?> GetByEmailAsync(string email)
        {
            return await _aspNetUsers.FirstOrDefaultAsync(x => x.Email == email);
        }
    }
}
