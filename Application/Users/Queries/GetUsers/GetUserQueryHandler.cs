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
  public class GetUserQueryHandler : IRequestHandler<GetUsersQuery, List<User>>
  {
    private readonly IUserRepository _userRepository;

    public GetUserQueryHandler(IUserRepository userRepository)
    {
      _userRepository = userRepository;
    }

    public Task<List<User>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
      return Task.FromResult(_userRepository.GetAllDapper());
    }
  }
}
