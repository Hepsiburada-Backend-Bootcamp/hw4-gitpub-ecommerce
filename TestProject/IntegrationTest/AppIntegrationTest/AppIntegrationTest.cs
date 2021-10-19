using Application.Requests.Orders;
using Application.Requests.Products;
using Application.Requests.Users;
using Domain.Commands.Users;
using Domain.Dtos.OderDetails;
using Domain.Dtos.OrderItems;
using Domain.Dtos.Orders;
using Domain.Dtos.Products;
using Domain.Dtos.Users;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace TestProject.IntegrationTest.AppIntegrationTest
{
  public class AppIntegrationTest : IClassFixture<CustomWebApiFactory>
  {
    private readonly HttpClient _client;
    private readonly CustomWebApiFactory _factory;

    public AppIntegrationTest(CustomWebApiFactory factory)
    {
      _factory = factory;
      _client = _factory.CreateClient();
    }

    [Fact]
    public async Task Order_WhenValidInputsAreGiven_ShouldBeCreatedAndReturnedSuccesfully()
    {
      #region CreateUser

      CreateUserRequest createUserRequest = new CreateUserRequest
      {
        Name = "Test Name 1",
        LastName = "Test Last Name 1",
        Email = "Test Email 1"
      };

      var createUserRequestJson = JsonSerializer.Serialize(createUserRequest);

      var createUserRequestStringContent = new StringContent(createUserRequestJson, Encoding.UTF8, "application/json");

      var createUserResponse = await _client.PostAsync("api/user", createUserRequestStringContent);

      var getAllUsersResponse = await _client.GetAsync("api/user");
      var getAllUsersResponseJsonString = await getAllUsersResponse.Content.ReadAsStringAsync();

      var users = JsonSerializer.Deserialize<List<UserDto>>(getAllUsersResponseJsonString, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
      var lastUser = users.OrderByDescending(x => x.CreatedOn).First();

      Assert.Equal(HttpStatusCode.OK, createUserResponse.StatusCode);
      Assert.NotNull(users);
      Assert.NotEmpty(users);
      Assert.Equal(createUserRequest.Name, lastUser.Name);

      #endregion

      #region CreateProducts

      var createProductRequest = new CreateProductRequest
      {
        Name = "Test Product Name",
        Price = 10,
        Description = "Test Product Desc"
      };

      var createProductRequestJson = JsonSerializer.Serialize(createProductRequest);
      var createProductRequestStringContent = new StringContent(createProductRequestJson, Encoding.UTF8, "application/json");

      var createProductResponse = await _client.PostAsync("api/product", createProductRequestStringContent);
      Assert.Equal(HttpStatusCode.OK, createProductResponse.StatusCode);

      var getAllProductsResponse = await _client.GetAsync("api/product");
      Assert.Equal(HttpStatusCode.OK, getAllProductsResponse.StatusCode);


      var productListData = await getAllProductsResponse.Content.ReadAsStringAsync();

      var products = JsonSerializer.Deserialize<List<ProductDto>>(productListData,
          new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

      Assert.NotEmpty(products);
      Assert.NotNull(products);

      var lastProduct = products.OrderByDescending(x => x.CreatedOn).First();
      Assert.Equal(lastProduct.Name, createProductRequest.Name);
      Assert.Equal(lastProduct.Price, createProductRequest.Price);
      Assert.Equal(lastProduct.Description, createProductRequest.Description);

      #endregion

      #region CreateOrder


      List<OrderItemDto> orderItemDtos = new List<OrderItemDto>()
      {
         new OrderItemDto()
         {
           ProductId = lastProduct.Id, Quantity = 3
         }
      };

      var createOrderRequest = new CreateOrderRequest()
      {
        UserId = lastUser.Id,
        OrderItems = orderItemDtos
      };

      var createOrderRequestJson = JsonSerializer.Serialize(createOrderRequest);
      var createOrderRequestStringContent = new StringContent(createOrderRequestJson, Encoding.UTF8, "application/json");

      var createOrderResponse = await _client.PostAsync("api/Order", createOrderRequestStringContent);

      var getAllOrdersResponse = await _client.GetAsync("api/Order");

      var orderListData = await getAllOrdersResponse.Content.ReadAsStringAsync();
      var orders = JsonSerializer.Deserialize<List<OrderDto>>(orderListData,
          new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

      Assert.NotEmpty(orders);
      Assert.NotNull(orders);

      var lastOrder = orders.OrderByDescending(x => x.CreatedOn).First();

      lastOrder.OrderItems.Should().BeEquivalentTo(createOrderRequest.OrderItems);

      //Mongo order details control
      var getOrderByIdResponseMongo = await _client.GetAsync($"/api/Order/{lastOrder.Id}");
      Assert.Equal(HttpStatusCode.OK, getOrderByIdResponseMongo.StatusCode);

      var orderDataMongo = await getOrderByIdResponseMongo.Content.ReadAsStringAsync();
      var orderMongo = JsonSerializer.Deserialize<OrderDetailsDto>(orderDataMongo,
          new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

      Assert.NotNull(orderMongo);
      Assert.Equal(createOrderRequest.UserId, orderMongo.User.Id);


      lastProduct.Name.Should().BeEquivalentTo(orderMongo.OrderItems[0].Product.Name);
      lastProduct.Price.Should().Be(orderMongo.OrderItems[0].Product.Price);
      lastOrder.TotalPrice.Should().Be(orderMongo.TotalPrice);

      lastOrder.OrderItems[0].Quantity.Should().Be(orderMongo.OrderItems[0].Quantity);


      #endregion
    }
  }
}
