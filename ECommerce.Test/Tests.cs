using System;
using Moq;
using Xunit;


namespace ECommerce.Test
{
    public class Tests
    {
        [Fact]
        public void GetAll_OrderList()
        {
            //Arrange
            var orderRepoMock = new Mock<IOrderDetailsMongoRepository>();
            
            Assert.True(true);
        }
    }
}