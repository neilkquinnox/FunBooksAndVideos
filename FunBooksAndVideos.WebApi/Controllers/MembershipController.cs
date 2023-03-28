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
    public class MembershipController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMemoryCache _memoryCache;

        public MembershipController(IMemoryCache memoryCache, IMediator mediator)
        {
            _memoryCache = memoryCache;
            _mediator = mediator;
        }

        /// <summary>
        /// Creates a New Membership.
        /// </summary>
        /// <param name="membershipRequest"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(MembershipRequest membershipRequest)
        {
            CreateMembershipValidator validator = new CreateMembershipValidator();
            var result = validator.Validate(membershipRequest);
            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }
            return Ok(await _mediator.Send(new CreateMembershipCommand { Name = membershipRequest.Name, Description = membershipRequest.Description, Price = membershipRequest.Price }));
        }

        /// <summary>
        /// Gets all Memberships.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var cacheKey = "membershipsList";
            if (!_memoryCache.TryGetValue(cacheKey, out IEnumerable<MembershipEntity> memberships))
            {
                memberships = await _mediator.Send(new GetAllMembershipsQuery());
                var cacheExpiryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddSeconds(50),
                    Priority = CacheItemPriority.High,
                    SlidingExpiration = TimeSpan.FromSeconds(20)
                };
                _memoryCache.Set(cacheKey, memberships, cacheExpiryOptions);
            }
            return Ok(memberships);
        }

        /// <summary>
        /// Gets Membership Entity by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _mediator.Send(new GetMembershipByIdQuery { Id = id }));
        }

        /// <summary>
        /// Deletes Membership Entity based on Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _mediator.Send(new DeleteMembershipByIdCommand { Id = id }));
        }

        /// <summary>
        /// Updates the Membership Entity based on Id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="membershipRequest"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, MembershipRequest membershipRequest)
        {
            return Ok(await _mediator.Send(new UpdateMembershipCommand { Name = membershipRequest.Name, Description = membershipRequest.Description, Price = membershipRequest.Price }));
        }
    }
}