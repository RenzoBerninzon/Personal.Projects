using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Store.BusinessMS.Users.Domain.User;
using Store.BusinessMS.Users.Infrastructure.Database;
using Store.BusinessMS.Users.Application.Interfaces.Repositories;

namespace Store.BusinessMS.Users.Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<ApplicationUser, StoreDbContext>, IUserRepository
    {
        public readonly DbSet<ApplicationUser> _aspNetUsers;
        private readonly ILogger _logger;

        public UserRepository(ILoggerFactory logger, StoreDbContext context) : base(context)
        {
            _aspNetUsers = _context.Set<ApplicationUser>();
            _logger = logger.CreateLogger<UserRepository>();
        }

        public async Task<List<ApplicationUser?>> GetUsers(string? docNumber)
        {
            var query = _aspNetUsers.AsNoTracking().AsQueryable();

            if (!string.IsNullOrEmpty(docNumber))
            {
                query = query.Where(u => u.DocNumber == docNumber);
            }

            var response = await query.ToListAsync();
            return response;
        }

        public async Task<ApplicationUser?> GetById(string id)
        {
            return await _aspNetUsers.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<ApplicationUser?> GetByEmailAsync(string email)
        {
            return await _aspNetUsers.FirstOrDefaultAsync(x => x.Email == email);
        }
    }
}
