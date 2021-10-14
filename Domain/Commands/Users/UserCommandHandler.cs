using Core.Entities;
using Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Commands.Users
{
    public class UserCommandHandler : IRequestHandler<CreateUserCommand>
    {
        private readonly IUserRepository _userRepository;

        public UserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<Unit> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            User user = new User(request.Name, request.LastName, request.Email);
            _userRepository.CreateDapper(user);

            return Unit.Task;
        }
    }
}
