using Store.BusinessMS.Users.Application.Core;
using Store.BusinessMS.Users.Domain.User;

namespace Store.BusinessMS.Users.Application.Interfaces.Repositories
{
    public interface IUserRepository : IGenericRepository<ApplicationUser>
    {
        Task<PagedList<ApplicationUser?>> GetAllUsers(int pageNumber, int pageSize);
        Task<ApplicationUser?> GetByEmailAsync(string email);
    }
}
