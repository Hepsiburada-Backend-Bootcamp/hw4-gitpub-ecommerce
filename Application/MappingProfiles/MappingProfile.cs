using Application.Products.Queries;
using Application.Users.Queries.GetUsers;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MappingProfiles
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      CreateMap<User, GetUsersResponse>();
      CreateMap<Product, GetProductsResponse>();
    }
  }
}
