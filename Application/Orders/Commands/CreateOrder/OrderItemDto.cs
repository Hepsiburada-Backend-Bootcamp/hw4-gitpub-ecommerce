using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Orders.Commands.CreateOrder
{
  public class OrderItemDto
  {
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
  }
}
