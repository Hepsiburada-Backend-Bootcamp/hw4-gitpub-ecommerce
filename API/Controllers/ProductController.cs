using Application.Requests.Products;
using AutoMapper;
using Domain.Commands.Products;
using Domain.Queries.Products;
using MediatR;
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
    public class ProductController : ApiControllerBase
    {
        public ProductController(IMapper mapper) : base(mapper)
        {
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CreateProductRequest request)
        {
            return Ok(await Mediator.Send(_mapper.Map<CreateProductCommand>(request)));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await Mediator.Send(new GetProductsQuery()));
        }
    }
}
