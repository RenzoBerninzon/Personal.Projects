using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Store.BusinessMS.Users.Application.Dtos;
using Store.BusinessMS.Users.Application.Interfaces.Repositories;
using Store.BusinessMS.Users.Domain.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.BusinessMS.Users.Application.Query
{
    public class GetUserById
    {
        public class Query : IRequest<GetByIdDto>
        {
            public Query(string id)
            {
                Id = id;
            }

            public string Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, GetByIdDto>
        {
            private readonly ILogger<GetUserById> _logger;
            private readonly IMapper _mapper;
            private readonly IUserRepository _userRepository;

            public Handler(ILogger<GetUserById> logger, IMapper mapper, IUserRepository userRepository)
            {
                _userRepository = userRepository;
                _logger = logger;
                _mapper = mapper;
            }
            public async Task<GetByIdDto> Handle(Query request, CancellationToken cancellationToken)
            {
                // Gets user from AppUser
                var obj = await _userRepository.GetByIdAsync(request.Id);

                if (obj == null)
                {
                    throw new InvalidOperationException("User isn´t registered, please validate");
                }

                var result = _mapper.Map<ApplicationUser, GetByIdDto>(obj);
                return result;
            }
        }
    }
}
