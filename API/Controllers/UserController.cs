using Application.Requests.Users;
using AutoMapper;
using Domain.Commands.Users;
using Domain.Dtos.Users;
using Domain.Queries.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ApiControllerBase
    {
        public UserController(IMapper mapper) : base(mapper)
        {
        }

        [HttpGet]
        public async Task<ActionResult<List<UserDto>>> GetAll()
        {
            return Ok(await Mediator.Send(new GetUsersQuery()));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserRequest request)
        {
            return Ok(await Mediator.Send(_mapper.Map<CreateUserCommand>(request)));
        }
    }
}
