using Core.Entities;
using Core.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class OrderDetailsMongoRepository : IOrderDetailsMongoRepository
    {
        private const string _eCommerceDb = "ECommerceDb";
        private const string _orderDetailCollection = "OrderDetails";
        private IMongoDatabase _dbContext;

        public OrderDetailsMongoRepository()
        {
            MongoClient _client = new MongoClient("mongodb://localhost:27017");
            _dbContext = _client.GetDatabase(_eCommerceDb);
        }

        public List<OrderDetail> GetAllOrderDetails()
        {
            return _dbContext.GetCollection<OrderDetail>(_orderDetailCollection).Find(_ => true).ToList();
        }
        
        public List<OrderDetail> GetOrderDetailByUserId(Guid userId)
        {
            return _dbContext.GetCollection<OrderDetail>(_orderDetailCollection).Find(x => x.User.Id == userId).ToList();
        }

        public OrderDetail GetOrderDetailByOrderId(Guid orderId)
        {
            return _dbContext.GetCollection<OrderDetail>(_orderDetailCollection).Find(x => x.OrderId == orderId).SingleOrDefault();
        }

        public void AddOrderDetail(OrderDetail order)
        {
            _dbContext.GetCollection<OrderDetail>(_orderDetailCollection).InsertOne(order);
        }
    }
}
