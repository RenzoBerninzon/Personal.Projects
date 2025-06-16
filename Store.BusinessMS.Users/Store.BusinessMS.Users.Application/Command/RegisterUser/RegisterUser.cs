using MediatR;
using Store.BusinessMS.Users.Application.Dtos;
using Store.BusinessMS.Users.Application.Command.RegisterUser.Request;
using Store.BusinessMS.Users.Application.Interfaces.Repositories;
using Microsoft.AspNetCore.Identity;
using Store.BusinessMS.Users.Application.Util;
using Store.BusinessMS.Users.Domain.User;

namespace Store.BusinessMS.Users.Application.Commands.RegisterUser
{
    public class RegisterUser
    {
        public class Command : IRequest<UserDto>
        {
            public RegisterUserRequest User { get; }

            public Command(RegisterUserRequest request)
            {
                User = request;
            }
        }

        public class Handler : IRequestHandler<Command, UserDto>
        {
            private readonly IGenericRepository<ApplicationUser> _userRepository;
            private readonly IPasswordHasher<ApplicationUser> _passwordHasher;

            public Handler(IUserRepository userRepository, IPasswordHasher<ApplicationUser> passwordHasher)
            {
                _userRepository = userRepository;
                _passwordHasher = passwordHasher;
            }

            public async Task<UserDto> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = new ApplicationUser
                {
                    Id = Guid.NewGuid().ToString(),
                    Email = request.User.Email,
                    NormalizedEmail = request.User.Email.ToUpperInvariant(),
                    Name = request.User.Name,
                    UserName = request.User.Email,
                    NormalizedUserName =  request.User.Email.ToUpperInvariant(),
                    LastName = request.User.LastName,
                    MothersLastName = request.User.MothersLastName,
                    DocNumber = request.User.DocNumber,
                    DocTypeId = request.User.DocTypeId,
                    PhoneNumber = request.User.PhoneNumber,
                    Gender = request.User.Gender,
                    CreatedOn = GeneralUtils.GetPeruvianTime(),
                    HasBoughtProducts = false,
                    UtmSource = request.User.UtmSource,
                    UtmMedium = request.User.UtmMedium,
                    UtmCampaign = request.User.UtmCampaign,
                    UtmTerm = request.User.UtmTerm,
                    UtmContent = request.User.UtmContent
                };

                user.PasswordHash = _passwordHasher.HashPassword(user, request.User.Password);

                await _userRepository.AddAsync(user);

                return new UserDto
                {
                    Id = user.Id,
                    Email = user.Email!,
                    Name = user.Name,
                    LastName = user.LastName,
                    MothersLastName = user.MothersLastName,
                    DocTypeId = user.DocTypeId,
                    DocNumber = user.DocNumber,
                    PhoneNumber = user.PhoneNumber,
                    HasBoughtProducts = user.HasBoughtProducts,
                };
            }
        }
    }
}
