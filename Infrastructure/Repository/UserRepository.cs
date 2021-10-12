using Dapper;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
  public class UserRepository : BaseRepository, IUserRepository
  {
    private readonly ECommerceDbContext _dbContext;
    private readonly IConfiguration _configuration;

    public UserRepository(ECommerceDbContext dbContext, IConfiguration configuration) : base(configuration)
    {
      _dbContext = dbContext;
      _configuration = configuration;
    }

    public List<User> GetAll()
    {
      return _dbContext.Users.Include(user => user.Orders).ToList();
    }

    public List<User> GetAllDapper()
    {
      try
      {
        var query = @"SELECT * FROM ""Users"" ";

        using(var connection = CreateConnection())
        {
          if(connection.State != System.Data.ConnectionState.Open)
          {
            connection.Open();
          }

          return connection.Query<User>(query).ToList();
        }

      }
      catch(Exception ex)
      {
        throw new Exception(ex.Message, ex);
      }
    }

    public void Create(User user)
    {
      _dbContext.Users.Add(user);
      _dbContext.SaveChanges();
    }

    public void CreateDapper(User user)
    {
      try
      {
        var query = @"INSERT INTO ""Users"" (""Id"", ""Name"", ""LastName"", ""Email"") VALUES (@Id, @Name, @LastName, @Email)";

        var parameters = new DynamicParameters();
        parameters.Add("Id", Guid.NewGuid());
        parameters.Add("Name", user.Name);
        parameters.Add("LastName", user.LastName);
        parameters.Add("Email", user.Email);

        using(var connection = CreateConnection())
        {
          if(connection.State != System.Data.ConnectionState.Open)
          {
            connection.Open();
          }

          connection.Execute(query, parameters);
        }

      }
      catch(Exception ex)
      {
        throw new Exception(ex.Message, ex);
      }
    }

  }
}
