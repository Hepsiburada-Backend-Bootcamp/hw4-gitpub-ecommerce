using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class User : BaseEntity
    {
        public User(string name, string lastName, string email)
        {
            Name = name;
            LastName = lastName;
            Email = email;
        }
        public string Name { get; protected set; }
        public string LastName { get; protected set; }
        public string FullName => $"{Name} {FullName}";
        public string Email { get; protected set; }
        public ICollection<Order> Orders { get; protected set; }
    }
}
