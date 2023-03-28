using FunBooksAndVideos.Infrastructure.Entities;
using FunBooksAndVideos.Infrastructure.Logging;
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
    public class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger _logger;
        private readonly IMemoryCache _memoryCache;

        public CustomerController(ILogger logger, IMemoryCache memoryCache, IMediator mediator)
        {
            _logger = logger;
            _memoryCache = memoryCache;
            _mediator = mediator;
        }

        /// <summary>
        /// Creates a New Customer.
        /// </summary>
        /// <param name="customerRequest"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(CustomerRequest customerRequest)
        {
            CreateCustomerValidator validator = new CreateCustomerValidator();
            var result = validator.Validate(customerRequest);
            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }
            _logger.LogInfo("Creating Customer at" + DateTime.Now);
            return Ok(await _mediator.Send(new CreateCustomerCommand { Name = customerRequest.Name, Email = customerRequest.Email, Phone = customerRequest.Phone }));
        }

        /// <summary>
        /// Gets all Customers.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var cacheKey = "customerList";
            if (!_memoryCache.TryGetValue(cacheKey, out IEnumerable<CustomerEntity> customers))
            {
                customers = await _mediator.Send(new GetAllCustomersQuery());
                var cacheExpiryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddSeconds(50),
                    Priority = CacheItemPriority.High,
                    SlidingExpiration = TimeSpan.FromSeconds(20)
                };
                _memoryCache.Set(cacheKey, customers, cacheExpiryOptions);
            }
            return Ok(customers);
        }

        /// <summary>
        /// Gets Customer Entity by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _mediator.Send(new GetCustomerByIdQuery { Id = id }));
        }

        /// <summary>
        /// Deletes Customer Entity based on Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _mediator.Send(new DeleteCustomerByIdCommand { Id = id }));
        }

        /// <summary>
        /// Updates the Customer Entity based on Id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="customerRequest"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CustomerRequest customerRequest)
        {
            if (id != customerRequest.Id)
            {
                _logger.LogError("Bad Update Request: Time" + DateTime.Now);
                return BadRequest();
            }
            return Ok(await _mediator.Send(new UpdateCustomerCommand { Id = customerRequest.Id, Name = customerRequest.Name, Email = customerRequest.Email, Phone = customerRequest.Phone }));
        }
    }
}