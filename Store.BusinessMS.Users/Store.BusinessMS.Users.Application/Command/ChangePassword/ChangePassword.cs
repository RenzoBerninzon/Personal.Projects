using MediatR;
using Microsoft.AspNetCore.Identity;
using Store.BusinessMS.Users.Application.Command.ChangePassword.Request;
using Store.BusinessMS.Users.Application.Core;
using Store.BusinessMS.Users.Application.Interfaces.Repositories;
using Store.BusinessMS.Users.Domain.Otp;
using Store.BusinessMS.Users.Domain.User;

namespace Store.BusinessMS.Users.Application.Command.ChangePassword
{
    public class ChangePassword
    {
        public class Command : IRequest<Result>
        {
            public ChangePasswordRequest Request { get; set; }

            public Command(ChangePasswordRequest request)
            {
                Request = request;
            }
        }

        public class Handler : IRequestHandler<Command, Result>
        {
            private readonly IOtpRepository _otpRepository;
            private readonly IUserRepository _userRepository;
            private readonly IPasswordHasher<ApplicationUser> _passwordHasher;

            public Handler(
                IOtpRepository otpRepository,
                IUserRepository userRepository,
                IPasswordHasher<ApplicationUser> passwordHasher)
            {
                _otpRepository = otpRepository;
                _userRepository = userRepository;
                _passwordHasher = passwordHasher;
            }

            public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
            {
                var req = request.Request;

                var user = await _userRepository.GetByIdAsync(req.UserId);
                if (user == null)
                {
                    return Result.Failure("User not found.");
                }

                var currentDateTime = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time"));

                var otp = await _otpRepository.GetValidOtpByUserAndCodeAsync(req.UserId, req.OtpCode);

                if (otp == null)
                {
                    return Result.Failure("OTP is invalid or has expired.");
                }

                user.PasswordHash = _passwordHasher.HashPassword(user, req.NewPassword);
                user.UpdatedOn = currentDateTime;

                await _userRepository.UpdateAsync(user);

                return Result.Success("Password changed successfully.");
            }
        }
    }
}
