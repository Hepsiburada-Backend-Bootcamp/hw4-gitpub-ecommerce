using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Orders.Commands.CreateOrder
{
  public class CreateOrderCommand : IRequest
  {
    public Guid UserId { get; set; }
    public List<OrderItemDto> OrderItems { get; set; }
  }
}
