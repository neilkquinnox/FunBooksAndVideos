using FunBooksAndVideos.Core.Models;
using FunBooksAndVideos.Infrastructure.Entities;
using FunBooksAndVideos.Service.Commands;
using FunBooksAndVideos.Service.Queries;
using FunBooksAndVideos.Service.Resources;
using FunBooksAndVideos.WebApi.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System.Collections.Generic;
using System.Threading;

namespace FunBooksAndVideos.UnitTests
{
    public class CustomerControllerTests
    {
        private Mock<IMediator> _mockMediator;
        private Mock<Infrastructure.Logging.ILogger> _mockLogger;
        private Mock<IMemoryCache> _mockMemoryCache;
        private CustomerController _controller;

        [SetUp]
        public void Setup()
        {
            _mockMediator = new Mock<IMediator>();

            _mockLogger = new Mock<Infrastructure.Logging.ILogger>();

            _mockMemoryCache = new Mock<IMemoryCache>();
            var mockCacheEntry = new Mock<ICacheEntry>();

            string keyPayload = null;
            _mockMemoryCache
             .Setup(mc => mc.CreateEntry(It.IsAny<object>()))
             .Callback((object k) => keyPayload = (string)k)
             .Returns(mockCacheEntry.Object);

            var customerData = new List<CustomerEntity>();
            customerData.Add(new CustomerEntity { Name = "Customer 1", Email = "test@mm.com", Phone = "2133323" });

            _mockMediator
            .Setup(m => m.Send(It.IsAny<CreateCustomerCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(1)
            .Verifiable("Send Success");

            _mockMediator
            .Setup(m => m.Send(It.IsAny<GetAllCustomersQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(customerData);

            _mockMediator
            .Setup(m => m.Send(It.IsAny<GetCustomerByIdQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new CustomerModel { Name = "Customer 1", Email = "test@mm.com", Phone = "2133323" });

            _mockMediator
           .Setup(m => m.Send(It.IsAny<DeleteCustomerByIdCommand>(), It.IsAny<CancellationToken>()))
           .ReturnsAsync(1);

            _mockMediator
           .Setup(m => m.Send(It.IsAny<UpdateCustomerCommand>(), It.IsAny<CancellationToken>()))
           .ReturnsAsync(1)
           .Verifiable("Send Success");

            _controller = new CustomerController(_mockLogger.Object, _mockMemoryCache.Object, _mockMediator.Object);
        }

        [Test]
        public void Test_Create_Customer_Should_Return_with_Success_Status()
        {
            var createCustomerReq = new CustomerRequest
            {
                Name = "Customer 1",
                Email = "Customer@testmail.com",
                Phone = "000-0000-0000"
            };

            var actionResult = _controller.Create(createCustomerReq);

            _mockMediator.Verify(x => x.Send(It.IsAny<CreateCustomerCommand>(), It.IsAny<CancellationToken>()), Times.Once());

            var okResult = actionResult.Result as ObjectResult;

            // Assert
            Assert.IsNotNull(okResult);
            Assert.That(okResult.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public void Test_Create_Customer_Should_Return_with_Bad_Request_Invalid_Input()
        {
            var createCustomerReq = new CustomerRequest
            {
                Name = "Customer 1",
                Email = "wrong_mail_format",
                Phone = "000-0000-0000"
            };

            var actionResult = _controller.Create(createCustomerReq);

            _mockMediator.Verify(x => x.Send(It.IsAny<CreateCustomerCommand>(), It.IsAny<CancellationToken>()), Times.Never());

            var badResult = actionResult.Result as ObjectResult;

            // Assert
            Assert.IsNotNull(badResult);
            Assert.That(badResult.StatusCode, Is.EqualTo(400));
        }

        [Test]
        public void Test_GetAll_Customer_Should_Return_with_Success_Status()
        {
            var actionResult = _controller.GetAll();

            _mockMediator.Verify(x => x.Send(It.IsAny<GetAllCustomersQuery>(), It.IsAny<CancellationToken>()), Times.Once());

            var okResult = actionResult.Result as ObjectResult;

            // Assert
            Assert.IsNotNull(okResult);
            Assert.That(okResult.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public void Test_Get_Customer_Should_Return_with_Success_Status()
        {
            var actionResult = _controller.GetById(1);

            _mockMediator.Verify(x => x.Send(It.IsAny<GetCustomerByIdQuery>(), It.IsAny<CancellationToken>()), Times.Once());
            var okResult = actionResult.Result as ObjectResult;
            // Assert
            Assert.That(okResult, Is.Not.Null);
            Assert.That(okResult.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public void Test_Delete_Customer_Should_Return_with_Success_Status()
        {
            var actionResult = _controller.Delete(1);

            _mockMediator.Verify(x => x.Send(It.IsAny<DeleteCustomerByIdCommand>(), It.IsAny<CancellationToken>()), Times.Once());
            var okResult = actionResult.Result as ObjectResult;
            // Assert
            Assert.That(okResult, Is.Not.Null);
            Assert.That(okResult.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public void Test_Update_Customer_Should_Return_with_Success_Status()
        {
            var updateCustomerReq = new CustomerRequest
            {
                Name = "Customer 1",
                Email = "tets@test.com",
                Phone = "23213213"
            };

            var actionResult = _controller.Update(0, updateCustomerReq);

            _mockMediator.Verify(x => x.Send(It.IsAny<UpdateCustomerCommand>(), It.IsAny<CancellationToken>()), Times.Once());

            var okResult = actionResult.Result as ObjectResult;

            // Assert
            Assert.That(okResult, Is.Not.Null);
            Assert.That(okResult.StatusCode, Is.EqualTo(200));
        }
    }
}