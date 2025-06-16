using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Store.BusinessMS.Users.Application.Core;
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
        public class Query : PagingParams, IRequest<PagedList<UserDto>>
        {
            public Query()
            {           
            }
        }
        public class Handler : IRequestHandler<Query, PagedList<UserDto>>
        {
            private readonly IMapper _mapper;
            private readonly IUserRepository _userRepository;
            public Handler(IMapper mapper, IUserRepository userRepository)
            {
                _userRepository = userRepository;
                _mapper = mapper;
            }
            public async Task<PagedList<UserDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var pagedUsers = await _userRepository.GetAllUsers(request.PageNumber, request.PageSize);

                var userDtos = _mapper.Map<List<UserDto>>(pagedUsers.Items);
                return new PagedList<UserDto>(userDtos, request.PageNumber, request.PageSize, pagedUsers.TotalCount);
            }
        }
    }
}

