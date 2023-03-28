using FunBooksAndVideos.Infrastructure.Entities;
using FunBooksAndVideos.Infrastructure.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FunBooksAndVideos.Service.Queries
{
    public class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery, IEnumerable<CustomerEntity>>
    {
        private readonly IGenericRepository<CustomerEntity> _context;

        public GetAllCustomersQueryHandler(IGenericRepository<CustomerEntity> context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CustomerEntity>> Handle(GetAllCustomersQuery query, CancellationToken cancellationToken)
        {
            return await _context.GetAll().ToListAsync();
        }
    }
}