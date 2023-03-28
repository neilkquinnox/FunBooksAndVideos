using FunBooksAndVideos.Infrastructure.Entities;
using FunBooksAndVideos.Service.Commands;
using FunBooksAndVideos.Service.Queries;
using FunBooksAndVideos.Service.Resources;
using FunBooksAndVideos.Service.Validators;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FunBooksAndVideos.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMemoryCache _memoryCache;

        public OrderController(IMemoryCache memoryCache, IMediator mediator)
        {
            _memoryCache = memoryCache;
            _mediator = mediator;
        }

        /// <summary>
        /// Creates a New Order.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(OrderRequest orderRequest)
        {
            CreateOrderValidator validator = new CreateOrderValidator();
            var result = validator.Validate(orderRequest);
            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }
            return Ok(await _mediator.Send(new OrderRequest { Customer_ID = orderRequest.Customer_ID, Date = orderRequest.Date, orderProducts = orderRequest.orderProducts }));
        }

        /// <summary>
        /// Gets all Orders.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var cacheKey = "orderList";
            if (!_memoryCache.TryGetValue(cacheKey, out IEnumerable<OrderEntity> orders))
            {
                orders = await _mediator.Send(new GetAllOrdersQuery());
                var cacheExpiryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddSeconds(50),
                    Priority = CacheItemPriority.High,
                    SlidingExpiration = TimeSpan.FromSeconds(20)
                };
                _memoryCache.Set(cacheKey, orders, cacheExpiryOptions);
            }
            return Ok(orders);
        }

        /// <summary>
        /// Gets Order Entity by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _mediator.Send(new GetOrderByIdQuery { Id = id }));
        }

        /// <summary>
        /// Deletes Order Entity based on Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _mediator.Send(new DeleteOrderByIdCommand { Id = id }));
        }
    }
}