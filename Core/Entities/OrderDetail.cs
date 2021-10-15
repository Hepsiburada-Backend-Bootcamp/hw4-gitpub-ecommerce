using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class OrderDetail
    {
        [BsonElement("Id")]
        public Guid OrderId { get; set; }

        [BsonElement("OrderItems")]
        public List<MongoOrderItem> OrderItems;
        [BsonElement("TotalPrice")]
        public int TotalPrice { get; set; }
        [BsonElement("User")]
        public User User { get; set; }
        [BsonElement("OrderDate")]
        public DateTime OrderDate { get; set; }
    }
    public class MongoProduct
    {
        [BsonElement("Name")]
        public string Name { get; set; }
        [BsonElement("Price")]
        public int Price { get; set; }
    }
    public class MongoOrderItem
    {
        [BsonElement("Product")]
        public MongoProduct Product{ get; set; }
        [BsonElement("Quantity")]
        public int Quantity { get; set; }
        [BsonElement("TotalPrice")]
        public int TotalPrice { get; set; }

    }
}
