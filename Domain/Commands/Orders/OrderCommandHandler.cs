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
        private readonly IOrderDetailsMongoRepository _orderdetailsRepository;
        private readonly IUserRepository _userRepository;

        public OrderCommandHandler(IOrderItemRepository orderItemRepository, IOrderRepository orderRepository, IProductRepository productRepository, IOrderDetailsMongoRepository orderdetailRepository, IUserRepository userRepository)
        {
            _orderItemRepository = orderItemRepository;
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _orderdetailsRepository = orderdetailRepository;
            _userRepository = userRepository;
        }

        public Task<Unit> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            Order order = new Order(request.UserId);

            OrderDetail orderDetail = new OrderDetail();
            MongoOrderItem mongoOrderItem = new MongoOrderItem();
            MongoProduct mongoProduct = new MongoProduct();
            List<MongoOrderItem> mongoOrderItemList = new List<MongoOrderItem>();


            int totalPrice = 0;
            _orderRepository.CreateDapper(order);

            foreach (var item in request.OrderItems)
            {
                var dbProduct = _productRepository.GetById(item.ProductId);
                OrderItem orderItem = new OrderItem(item.ProductId, order.Id, dbProduct.Price, item.Quantity);
                mongoProduct.Name = dbProduct.Name;
                mongoProduct.Price = dbProduct.Price;
                mongoOrderItem.Product = mongoProduct;
                mongoOrderItem.Quantity = item.Quantity;
                mongoOrderItem.TotalPrice = mongoOrderItem.TotalPrice + item.Quantity * dbProduct.Price;
                totalPrice = totalPrice + mongoOrderItem.TotalPrice;
                mongoOrderItemList.Add(mongoOrderItem);

                _orderItemRepository.CreateDapper(orderItem);
            }
            orderDetail.TotalPrice = totalPrice;
            orderDetail.User = _userRepository.GetById(request.UserId);
            orderDetail.OrderItems = mongoOrderItemList;
            orderDetail.OrderId = order.Id;
            orderDetail.OrderDate = DateTime.Now;
            _orderdetailsRepository.AddOrderDetail(orderDetail);


            return Unit.Task;
        }
    }
}
