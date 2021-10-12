using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
  public interface IRepository<TEntity> where TEntity : class
  {
    void Add(TEntity entity);
  }
}
