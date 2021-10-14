using Core.Interfaces;
using Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Commands.Orders
{
    public class OrderCommandHandler : IRequestHandler<CreateOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IProductRepository _productRepository;

        public OrderCommandHandler(IOrderItemRepository orderItemRepository, IOrderRepository orderRepository, IProductRepository productRepository)
        {
            _orderItemRepository = orderItemRepository;
            _orderRepository = orderRepository;
            _productRepository = productRepository;
        }

        public Task<Unit> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            Order order = new Order(request.UserId);

            _orderRepository.CreateDapper(order);

            foreach (var item in request.OrderItems)
            {
                var dbProduct = _productRepository.GetById(item.ProductId);
                OrderItem orderItem = new OrderItem(item.ProductId, order.Id, dbProduct.Price, item.Quantity);

                _orderItemRepository.CreateDapper(orderItem);
            }

            return Unit.Task;
        }
    }
}
