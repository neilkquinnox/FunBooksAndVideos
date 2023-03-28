using FunBooksAndVideos.Infrastructure.Entities;
using FunBooksAndVideos.Infrastructure.Repository;
using FunBooksAndVideos.Service.Commands;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using Service;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FunBooksAndVideos.UnitTests.Commands
{
    public class CustomerCommandTests
    {
        private Mock<IGenericRepository<CustomerEntity>> _mockDbContext;
        private Mock<DbSet<CustomerEntity>> _mockDbSetCustomers;

        [SetUp]
        public void Setup()
        {
            _mockDbContext = new Mock<IGenericRepository<CustomerEntity>>();

            _mockDbSetCustomers = new Mock<DbSet<CustomerEntity>>();

            var customerData = new List<CustomerEntity> { new CustomerEntity
            {
                Id = "1",
                Name = "Customer 1",
                Email = "Customer@testmail.com",
                Phone = "000-0000-0000"
            }
            }.AsQueryable();

            _mockDbSetCustomers.As<IAsyncEnumerable<CustomerEntity>>()
                .Setup(x => x.GetAsyncEnumerator(default))
                .Returns(new TestAsyncEnumerator<CustomerEntity>(customerData.GetEnumerator()));
            _mockDbSetCustomers.As<IQueryable<CustomerEntity>>()
            .Setup(m => m.Provider)
                .Returns(new TestAsyncQueryProvider<CustomerEntity>(customerData.Provider));

            _mockDbSetCustomers.As<IQueryable<CustomerEntity>>().Setup(m => m.Expression).Returns(customerData.Expression);
            _mockDbSetCustomers.As<IQueryable<CustomerEntity>>().Setup(m => m.ElementType).Returns(customerData.ElementType);
            _mockDbSetCustomers.As<IQueryable<CustomerEntity>>().Setup(m => m.GetEnumerator()).Returns(() => customerData.GetEnumerator());
        }

        [Test]
        public async Task Test_CreateCustomerCommand_Should_Create_Customer()
        {
            var createCustomerCommand = new CreateCustomerCommand
            {
                Name = "Customer 1",
                Email = "Customer@testmail.com",
                Phone = "000-0000-0000"
            };

            var handler = new CreateCustomerCommandHandler(_mockDbContext.Object);
            var result = await handler.Handle(createCustomerCommand, CancellationToken.None);
            Assert.NotNull(result);
            Assert.That(result, Is.InstanceOf<int>());
        }

        [Test]
        public async Task Test_DeleteCustomerByIdCommand_Should_Delete_Customer()
        {
            var command = new DeleteCustomerByIdCommand { Id = 1 };

            var handler = new DeleteCustomerByIdCommandHandler(_mockDbContext.Object);
            var result = await handler.Handle(command, CancellationToken.None);
            Assert.NotNull(result);
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.EqualTo(1));
                Assert.That(result, Is.InstanceOf<int>());
            });
        }

        [Test]
        public async Task Test_UpdateCustomerCommandHandler_Should_Update_Customer()
        {
            var command = new UpdateCustomerCommand { Id = 1 };

            var handler = new UpdateCustomerCommandHandler(_mockDbContext.Object);
            var result = await handler.Handle(command, CancellationToken.None);
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.EqualTo(1));
                Assert.That(result, Is.InstanceOf<int>());
            });
        }
    }
}