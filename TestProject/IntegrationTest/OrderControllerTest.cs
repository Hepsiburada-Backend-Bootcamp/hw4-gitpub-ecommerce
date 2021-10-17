using Application.Requests.Orders;
using Application.Requests.Products;
using Application.Requests.Users;
using Core.Entities;
using Domain.Dtos.Products;
using Domain.Dtos.Users;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using API;
using AutoMapper;
using Domain.Dtos.OderDetails;
using Domain.Dtos.OrderItems;
using Domain.Dtos.Orders;
using Xunit;
using FluentAssertions;
using Moq;

namespace TestProject.IntegrationTest
{
    public class OrderControllerTest : IClassFixture<ECommerceApiFactory> 
    {
        private readonly WebApplicationFactory<TestStartup> _factory;
        private readonly HttpClient _client;

        public OrderControllerTest(ECommerceApiFactory factory )
        {
            _factory = factory;
            _client = _factory.CreateClient(new WebApplicationFactoryClientOptions
                {BaseAddress = new System.Uri("https://localhost")});
        }


        [Fact]
        public async Task Post_Should_Return_Success()
        {
            #region user

            var userRequest = new CreateUserRequest {Name = "a", LastName = "b", Email = "c"};

            var jsonUser = JsonSerializer.Serialize(userRequest);
            var contentUser = new StringContent(jsonUser, Encoding.UTF8, "application/json");
            var clientUser = _factory.CreateClient();


            var responseUser = await clientUser.PostAsync("api/User/", contentUser);
            var actualStatusCodeUser = responseUser.StatusCode;

            Assert.Equal(HttpStatusCode.OK, actualStatusCodeUser);


            var responseGetAllUser = await clientUser.GetAsync("api/User");
            responseGetAllUser.EnsureSuccessStatusCode();

            var userListData = await responseGetAllUser.Content.ReadAsStringAsync();
            var userList = JsonSerializer.Deserialize<List<UserDto>>(userListData,
                new JsonSerializerOptions() {PropertyNameCaseInsensitive = true});

            Assert.NotEmpty(userList);
            Assert.NotNull(userList);

            var dbLatestUser = userList.OrderByDescending(x => x.CreatedOn).First();
            Assert.Equal(dbLatestUser.Name, userRequest.Name);
            Assert.Equal(dbLatestUser.LastName, userRequest.LastName);

            #endregion

            #region Products

            var productRequest = new CreateProductRequest
                {Name = "productname", Price = 10, Description = "descproduct"};

            var jsonProduct = JsonSerializer.Serialize(productRequest);
            var contentProduct = new StringContent(jsonProduct, Encoding.UTF8, "application/json");
            var clientProduct = _factory.CreateClient();


            var responseProduct = await clientProduct.PostAsync("api/Product", contentProduct);
            var actualStatusCodeProduct = responseProduct.StatusCode;

            Assert.Equal(HttpStatusCode.OK, actualStatusCodeProduct);


            var responseGetAllProduct = await clientProduct.GetAsync("api/Product");
            responseGetAllProduct.EnsureSuccessStatusCode();

            var productListData = await responseGetAllProduct.Content.ReadAsStringAsync();
            var productList = JsonSerializer.Deserialize<List<ProductDto>>(productListData,
                new JsonSerializerOptions() {PropertyNameCaseInsensitive = true});

            Assert.NotEmpty(productList);
            Assert.NotNull(productList);

            var dbLatestProduct = productList.OrderByDescending(x => x.CreatedOn).First();
            Assert.Equal(dbLatestProduct.Name, productRequest.Name);
            Assert.Equal(dbLatestProduct.Price, productRequest.Price);
            Assert.Equal(dbLatestProduct.Description, productRequest.Description);

            #endregion

            #region Order

            List<OrderItemDto> orderItemDtos = new List<OrderItemDto>()
            {
                new OrderItemDto() {ProductId = dbLatestProduct.Id, Quantity = 3}
            };

            var orderRequest = new CreateOrderRequest()
            {
                UserId = dbLatestUser.Id, OrderItems = orderItemDtos
            };

            var jsonOrder = JsonSerializer.Serialize(orderRequest);
            var contentOrder = new StringContent(jsonOrder, Encoding.UTF8, "application/json");
            var clientOrder = _factory.CreateClient();


            var responseOrder = await clientOrder.PostAsync("api/Order", contentOrder);
            var actualStatusCodeOrder = responseOrder.StatusCode;

            Assert.Equal(HttpStatusCode.OK, actualStatusCodeOrder);


            var responseGetAllOrder = await clientOrder.GetAsync("api/Order");
            responseGetAllOrder.EnsureSuccessStatusCode();

            var orderListData = await responseGetAllOrder.Content.ReadAsStringAsync();
            var orderList = JsonSerializer.Deserialize<List<OrderDto>>(orderListData,
                new JsonSerializerOptions() {PropertyNameCaseInsensitive = true});

            Assert.NotEmpty(orderList);
            Assert.NotNull(orderList);

            
            var dbLatestOrder = orderList.OrderByDescending(x => x.CreatedOn).First();

            dbLatestOrder.OrderItems.Should().BeEquivalentTo(orderRequest.OrderItems);

            //Mongo order details control
            var responseMongoOrder = await clientOrder.GetAsync($"/api/Order/{dbLatestOrder.Id}");
            responseMongoOrder.EnsureSuccessStatusCode();
            
            var orderMongoData = await responseMongoOrder.Content.ReadAsStringAsync();
            var mongoOrder = JsonSerializer.Deserialize<OrderDetailsDto>(orderMongoData,
                new JsonSerializerOptions() {PropertyNameCaseInsensitive = true});
            
            Assert.NotNull(mongoOrder);
            Assert.Equal(orderRequest.UserId, mongoOrder.User.Id);

        
            dbLatestProduct.Name.Should().BeEquivalentTo(mongoOrder.OrderItems[0].Product.Name);
            dbLatestProduct.Price.Should().Be(mongoOrder.OrderItems[0].Product.Price);

            dbLatestOrder.OrderItems[0].Quantity.Should().Be(mongoOrder.OrderItems[0].Quantity);

            dbLatestOrder.TotalPrice.Should().Be(mongoOrder.TotalPrice);
            
            #endregion

        }
    }
}