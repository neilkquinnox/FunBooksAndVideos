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
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMemoryCache _memoryCache;

        public ProductController(IMemoryCache memoryCache, IMediator mediator)
        {
            _memoryCache = memoryCache;
            _mediator = mediator;
        }

        /// <summary>
        /// Creates a New Product.
        /// </summary>
        /// <param name="productRequest"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(ProductRequest productRequest)
        {
            CreateProductValidator validator = new CreateProductValidator();
            var result = validator.Validate(productRequest);
            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }
            return Ok(await _mediator.Send(new CreateProductCommand { Barcode = productRequest.Barcode, Category = productRequest.Category, Description = productRequest.Description, Name = productRequest.Name, Price = productRequest.price }));
        }

        /// <summary>
        /// Gets all Products.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var cacheKey = "productList";
            if (!_memoryCache.TryGetValue(cacheKey, out IEnumerable<ProductEntity> products))
            {
                products = await _mediator.Send(new GetAllProductsQuery());
                var cacheExpiryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddSeconds(50),
                    Priority = CacheItemPriority.High,
                    SlidingExpiration = TimeSpan.FromSeconds(20)
                };
                _memoryCache.Set(cacheKey, products, cacheExpiryOptions);
            }
            return Ok(products);
        }

        /// <summary>
        /// Gets Product Entity by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _mediator.Send(new GetProductByIdQuery { Id = id }));
        }

        /// <summary>
        /// Deletes Product Entity based on Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _mediator.Send(new DeleteProductByIdCommand { Id = id }));
        }
    }
}