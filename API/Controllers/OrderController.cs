using Application.Requests.Orders;
using AutoMapper;
using Domain.Commands.Orders;
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
    public class OrderController : ApiControllerBase
    {
        public OrderController(IMapper mapper) : base(mapper)
        {
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrderRequest request)
        {
            return Ok(await Mediator.Send(_mapper.Map<CreateOrderCommand>(request)));
        }
    }
}
