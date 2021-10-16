using API;
using API.Configurations;
using Application;
using Core.Entities;
using Infrastructure;
using Infrastructure.Context;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using Xunit;

namespace IntegrationTest
{
    public class TestStartup : Startup
    {
        public TestStartup(IConfiguration configuration) : base(configuration)
        {
        }

        public override void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            env.EnvironmentName = "Test";
            var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ECommerceDbContext>();

            Seed(context);

            base.Configure(app, env);

        }

        public override void ConfigureServices(IServiceCollection services)
        {

            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddApplication();
            services.AddInfrastructure(Configuration);
            services.AddCommandQueryInjectionSetup();
            //object p = services.AddApplicationModuleTestDb(Configuration);
            services.AddDbContext<ECommerceDbContext>(
             options => options.UseInMemoryDatabase("TestDB")
         );
            services.AddControllers().AddApplicationPart(typeof(Startup).Assembly);
        }

        public void Seed(ECommerceDbContext context)
        {
            for (int i = 1; i < 10; i++)
            {
                User user = new User($"{i}. isim", $"{i}. soyisim", $"{i}. email");

                context.Users.Add(user);

                Product product = new Product( $"{i}. ürün", i * 2 + 10, null);

                context.Products.Add(product);

                context.SaveChanges();
            }
        }
    }
}
