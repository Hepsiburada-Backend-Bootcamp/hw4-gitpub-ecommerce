using Application.Products.Commands.CreateProduct;
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
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProductCommand command)
    {
      return Ok(await Mediator.Send(command));
    }
  }
}
