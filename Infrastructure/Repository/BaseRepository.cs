﻿using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
  public abstract class BaseRepository
  {
    private readonly IConfiguration _configuration;

    protected BaseRepository(IConfiguration configuration)
    {
      _configuration = configuration;
    }

    protected IDbConnection CreateConnection()
    {
      return new NpgsqlConnection("UserID=postgres;Password=12345;Server=localhost;Port=5432;Database=ECommerce;Integrated Security=true;Pooling=true;");
    }
  }
}
