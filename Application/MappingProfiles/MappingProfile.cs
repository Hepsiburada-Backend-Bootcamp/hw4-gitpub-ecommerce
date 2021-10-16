using Application.Requests.Orders;
using Application.Requests.Products;
using Application.Requests.Users;
using AutoMapper;
using Core.Entities;
using Domain.Commands.Orders;
using Domain.Commands.Products;
using Domain.Commands.Users;
using Domain.Dtos.OderDetails;
using Domain.Dtos.Products;
using Domain.Dtos.Users;
using Domain.Queries.OrderDetails;
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
            #region DomainToResponse

            CreateMap<User, UserDto>();
            CreateMap<UserDetail,UserDto>();
            CreateMap<Product, ProductDto>();
            CreateMap<User, UserDetail>()
                .ForMember(dest => dest.Id, opt =>
                    opt.MapFrom(src => src.Id.ToString()));
            CreateMap<OrderDetail, OrderDetailsDto>();

            #endregion

            #region RequestToDomain

            CreateMap<CreateOrderRequest, CreateOrderCommand>();
            CreateMap<CreateProductRequest, CreateProductCommand>();
            CreateMap<CreateProductRequest, CreateProductDapperCommand>();
            CreateMap<CreateUserRequest, CreateUserCommand>();
            CreateMap<GetOrderByIdRequest, GetOrderByIdQuery>();

            CreateMap<CreateUserRequest, CreateUserDapperCommand>();


            #endregion




        }
    }
}
