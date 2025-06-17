using Store.BusinessMS.Users.Domain.Otp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.BusinessMS.Users.Application.Interfaces.Repositories
{
    public interface IOtpRepository
    {
        Task<Otp?> GetValidOtpByUserAndCodeAsync(string userId, string code);
    }
}
