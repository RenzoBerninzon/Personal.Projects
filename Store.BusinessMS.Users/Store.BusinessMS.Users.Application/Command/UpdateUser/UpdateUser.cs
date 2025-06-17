using MediatR;
using Store.BusinessMS.Users.Application.Command.UpdateUser.Request;
using Store.BusinessMS.Users.Application.Interfaces.Repositories;
using Store.BusinessMS.Users.Domain.User;
using Store.BusinessMS.Users.Application.Core;
using Store.BusinessMS.Users.Application.Util;
using Store.BusinessMS.Users.Application.Dtos;
using Store.BusinessMS.Users.Application.Command.RegisterUser.Request;

namespace Store.BusinessMS.Users.Application.Command.UpdateUser
{
    public class UpdateUser
    {
        public class Command : UpdateUserRequest, IRequest<Result>
        {
        }

        public class Handler : IRequestHandler<Command, Result>
        {
            private readonly IGenericRepository<ApplicationUser> _userRepository;

            public Handler(IGenericRepository<ApplicationUser> userRepository)
            {
                _userRepository = userRepository;
            }

            public async Task<Result> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = await _userRepository.GetByIdAsync(request.Id);

                if (user is null)
                {
                    return new Result
                    {
                        Succeded = false,
                        Message = "User not found."
                    };
                }

                user.Name = request.Name ?? user.Name;
                user.LastName = request.LastName ?? user.LastName;
                user.MothersLastName = request.MothersLastName ?? user.MothersLastName;
                user.PhoneNumber = request.PhoneNumber ?? user.PhoneNumber;
                user.Gender = request.Gender ?? user.Gender;
                user.UpdatedOn = GeneralUtils.GetPeruvianTime();

                await _userRepository.UpdateAsync(user);
                return new Result
                {
                    Succeded = true,
                    Message = "User updated."
                };
            }
        }
    }
}
