using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Users.Queries.GetUsers
{
  public class GetUserQueryHandler : IRequestHandler<GetUsersQuery, List<GetUsersResponse>>
  {
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetUserQueryHandler(IUserRepository userRepository, IMapper mapper)
    {
      _userRepository = userRepository;
      _mapper = mapper;
    }

    public Task<List<GetUsersResponse>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
      var users = _userRepository.GetAll();
      var result = _mapper.Map<List<GetUsersResponse>>(users);
      return Task.FromResult(result);
    }
  }
}
