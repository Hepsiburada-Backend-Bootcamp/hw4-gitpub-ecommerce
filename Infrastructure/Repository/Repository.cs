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
  public abstract class Repository<TEntity> : BaseRepository, IRepository<TEntity> where TEntity : class
  {
    private readonly ECommerceDbContext _dbContext;
    private DbSet<TEntity> _dbSet;

    public Repository(ECommerceDbContext dbContext, IConfiguration configuration) : base(configuration)
    {
      _dbContext = dbContext;
      _dbSet = _dbContext.Set<TEntity>();
    }
    public void Add(TEntity entity)
    {
    }
  }
}
