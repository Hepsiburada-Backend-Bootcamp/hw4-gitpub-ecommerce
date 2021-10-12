using Application.Users.Commands.CreateUser;
using Application.Users.Queries.GetUsers;
using Domain.Entities;
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

    [HttpGet]
    public async Task<ActionResult<List<User>>> GetAll()
    {
      GetUsersQuery query = new();
      return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserCommand command)
    {
      return Ok(await Mediator.Send(command));
    }
  }
}
