using System;
using System.Collections.Generic;
using Domain.Dtos.OrderItems;
using MediatR;

namespace Domain.Commands.Orders
{
    public class CreateOrderDapperCommand: IRequest
    {
        public CreateOrderDapperCommand(Guid userId, List<OrderItemDto> orderItems)
        {
            UserId = userId;
            OrderItems = orderItems;
        }
        public Guid UserId { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }
    }
}