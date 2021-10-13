using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
  public interface IProductRepository : IRepository<Product>
  {
    void CreateDapper(Product product);
    List<Product> GetProductsDapper();
    Product GetById(Guid productId);
  }
}
