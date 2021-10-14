using Domain.Commands.Orders;
using Domain.Commands.Products;
using Domain.Commands.Users;
using Domain.Dtos.Products;
using Domain.Dtos.Users;
using Domain.Queries.Products;
using Domain.Queries.Users;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public static class CommandQueryInjector
    {
        public static void AddCommandQueryInjection(this IServiceCollection services)
        {
            #region Commands

            services.AddScoped<IRequestHandler<CreateOrderCommand, MediatR.Unit>, OrderCommandHandler>();
            services.AddScoped<IRequestHandler<CreateProductCommand, MediatR.Unit>, ProductCommandHandler>();
            services.AddScoped<IRequestHandler<CreateUserCommand, MediatR.Unit>, UserCommandHandler>();

            #endregion

            #region Queries

            services.AddScoped<IRequestHandler<GetProductsQuery, List<ProductDto>>, ProductQueryHandler>();
            services.AddScoped<IRequestHandler<GetUsersQuery, List<UserDto>>, UserQueryHandler>();

            #endregion
        }
    }
}
