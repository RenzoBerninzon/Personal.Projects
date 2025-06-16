using Store.BusinessMS.Users.Domain.User;

namespace Store.BusinessMS.Users.Application.Interfaces.Repositories
{
    public interface IUserRepository : IGenericRepository<ApplicationUser>
    {
        Task<List<ApplicationUser?>> GetUsers(string docNumber);
        Task<ApplicationUser?> GetById(string id);
        Task<ApplicationUser?> GetByEmailAsync(string email);
    }
}
