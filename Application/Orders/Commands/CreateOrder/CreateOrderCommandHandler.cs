using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Orders.Commands.CreateOrder
{
  public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand>
  {
    private readonly IOrderRepository _orderRepository;
    private readonly IOrderItemRepository _orderItemRepository;
    private readonly IProductRepository _productRepository;

    public CreateOrderCommandHandler(IOrderItemRepository orderItemRepository, IOrderRepository orderRepository, IProductRepository productRepository)
    {
      _orderItemRepository = orderItemRepository;
      _orderRepository = orderRepository;
      _productRepository = productRepository;
    }

    public Task<Unit> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
      Guid OrderId = Guid.NewGuid();
      Order order = new Order()
      {
        Id = OrderId,
        UserId = request.UserId,
        CreatedAt = DateTime.Now
      };

      _orderRepository.CreateDapper(order);

      OrderItem orderItem = new OrderItem();
      foreach(var orderItemDto in request.OrderItems)
      {
        orderItem.OrderId = OrderId;
        orderItem.ProductId = orderItemDto.ProductId;
        orderItem.Price = _productRepository.GetById(orderItemDto.ProductId).Price;
        orderItem.Quantity = orderItemDto.Quantity;

        _orderItemRepository.CreateDapper(orderItem);
      }

      return Unit.Task;
    }
  }
}
