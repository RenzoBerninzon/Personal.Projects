using MediatR;
using Store.BusinessMS.Users.Application.Command.CreateOtp.Response;
using Store.BusinessMS.Users.Application.Dtos;
using Store.BusinessMS.Users.Application.Interfaces.Repositories;
using Store.BusinessMS.Users.Domain.Otp;
using Store.BusinessMS.Users.Domain.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.BusinessMS.Users.Application.Command.CreateOtp
{
    public class CreateOtp
    {
        public class Command : IRequest<ResponseOtp>
        {
            public OtpDto Dto { get; set; }
            public Command(OtpDto dto)
            {
                Dto = dto;
            }
        }
        public class Handler : IRequestHandler<Command, ResponseOtp>
        {
            private readonly IGenericRepository<Otp> _otpRepository;
            private readonly IUserRepository _userRepository;
            public Handler(IGenericRepository<Otp> otpRepository, IUserRepository userRepository)
            {
                _otpRepository = otpRepository;
                _userRepository = userRepository;
            }
            public async Task<ResponseOtp> Handle(Command request, CancellationToken cancellationToken)
            {

                ApplicationUser? user = null;

                if (!string.IsNullOrEmpty(request.Dto.UserId))
                {
                    user = await _userRepository.GetByIdAsync(request.Dto.UserId);
                }
                else if (!string.IsNullOrEmpty(request.Dto.Email))
                {
                    user = await _userRepository.GetByEmailAsync(request.Dto.Email);
                }

                if (user == null) throw new InvalidOperationException("UserId o Email son requeridos.");

                Random code = new Random();
                string r = code.Next(0, 100000).ToString("D5");

                var currentDateTime = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time"));

                DateTime finalExpirationDate;
                if (request.Dto.ExpirationDate.HasValue && request.Dto.ExpirationDate.Value > currentDateTime)
                {
                    finalExpirationDate = request.Dto.ExpirationDate.Value;
                }
                else
                {
                    finalExpirationDate = currentDateTime.AddMinutes(10);
                }

                var otpObj = new Otp()
                {
                    UserId = user.Id?.ToString(),
                    Code = r,
                    CreatedOn = currentDateTime,
                    ExpirationDate = finalExpirationDate
                };

                await _otpRepository.AddAsync(otpObj);
                ResponseOtp responseOtp = new ResponseOtp { Code = r };
                return responseOtp;
            }
        }
    }
}
