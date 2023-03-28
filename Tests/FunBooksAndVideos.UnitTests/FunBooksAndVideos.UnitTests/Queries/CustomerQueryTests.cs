using AutoMapper;
using FunBooksAndVideos.Infrastructure.Entities;
using FunBooksAndVideos.Infrastructure.Repository;
using FunBooksAndVideos.Service.Queries;
using Moq;
using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;

namespace FunBooksAndVideos.UnitTests.Queries
{
    public class CustomerQueryTests
    {
        private Mock<IGenericRepository<CustomerEntity>> _mockDbContext;

        [SetUp]
        public void Setup()
        {
            _mockDbContext = new Mock<IGenericRepository<CustomerEntity>>();

            _mockDbContext.Setup(m => m.GetById(It.IsAny<int>())).ReturnsAsync(new CustomerEntity
            {
                Id = "1",
                Name = "Customer 1",
                Email = "Customer@testmail.com",
                Phone = "000-0000-0000"
            });
        }

        [Test]
        public async Task Test_GetCustomerByIdQueryHandler_Should_Get_Customer()
        {
            var query = new GetCustomerByIdQuery { Id = 1 };
            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(m => m.Map<Core.Models.CustomerModel>(It.IsAny<CustomerEntity>())).Returns(new Core.Models.CustomerModel { Id = "1" });

            var handler = new GetCustomerByIdQueryHandler(_mockDbContext.Object, mockMapper.Object);
            var result = await handler.Handle(query, CancellationToken.None);
            Assert.Multiple(() =>
            {
                Assert.That(result.Id, Is.EqualTo("1"));
                Assert.That(result.Id, Is.InstanceOf<string>());
            });
        }
    }
}