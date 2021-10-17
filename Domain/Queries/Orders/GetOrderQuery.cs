using System.Collections.Generic;
using Domain.Dtos.Orders;
using Domain.Dtos.Products;
using MediatR;

namespace Domain.Queries.Orders
{
    public class GetOrderQuery : IRequest<List<OrderDto>>
    {
        
    }
}