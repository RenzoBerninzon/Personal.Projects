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

namespace Store.BusinessMS.Users.Application.Query.GetUsers
{
    public class GetUsers
    {
        public class Query : IRequest<List<UserDto>>
        {
            public Query(string docNumber)
            {
                DocNumber = docNumber;
            }

            public string DocNumber { get; set; }
        }
        public class Handler : IRequestHandler<Query, List<UserDto>>
        {
            private readonly ILogger _logger;
            private readonly IMapper _mapper;
            private readonly IUserRepository _userRepository;
            public Handler(ILoggerFactory logger, IMapper mapper, IUserRepository userRepository)
            {
                _userRepository = userRepository;
                _logger = logger.CreateLogger<GetUsers>();
                _mapper = mapper;
            }
            public async Task<List<UserDto>> Handle(Query request, CancellationToken cancellationToken)
            {

                List<ApplicationUser?> obj = null;

                obj = await _userRepository.GetUsers(request.DocNumber);

                return _mapper.Map<List<ApplicationUser>, List<UserDto>>(obj); ;
            }
        }
    }
}

