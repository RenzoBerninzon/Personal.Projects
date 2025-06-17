using Microsoft.EntityFrameworkCore;
using Store.BusinessMS.Users.Application.Interfaces.Repositories;
using Store.BusinessMS.Users.Domain.Otp;
using Store.BusinessMS.Users.Infrastructure.Database;
using Store.BusinessMS.Users.Application.Util;

namespace Store.BusinessMS.Users.Infrastructure.Repositories
{
    public class OtpRepository : GenericRepository<Otp, StoreDbContext>, IOtpRepository
    {
        private readonly StoreDbContext _context;

        public OtpRepository(StoreDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Otp?> GetValidOtpByUserAndCodeAsync(string userId, string code)
        {
            var currentDateTime = GeneralUtils.GetPeruvianTime();

            return await _context.Otp
                .Where(o => o.UserId == userId && o.Code == code && o.ExpirationDate >= currentDateTime)
                .FirstOrDefaultAsync();
        }
    }
}
