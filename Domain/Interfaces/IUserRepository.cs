using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
  public interface IUserRepository
  {
    List<User> GetAll();
    void Create(User user);
    void CreateDapper(User user);
    List<User> GetAllDapper();
  }
}
