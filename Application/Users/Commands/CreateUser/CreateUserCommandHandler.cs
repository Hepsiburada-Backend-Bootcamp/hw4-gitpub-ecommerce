using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Users.Commands.CreateUser
{
  public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand>
  {
    private readonly IUserRepository _userRepository;

    public CreateUserCommandHandler(IUserRepository userRepository)
    {
      _userRepository = userRepository;
    }

    public Task<Unit> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
      User user = new User
      {
        Name = request.Name,
        LastName = request.LastName,
        Email = request.Email
      };

      _userRepository.CreateDapper(user);

      return Unit.Task;
    }
  }
}
