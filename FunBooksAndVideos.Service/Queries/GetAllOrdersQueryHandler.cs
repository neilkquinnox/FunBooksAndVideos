using FunBooksAndVideos.Infrastructure.Entities;
using FunBooksAndVideos.Infrastructure.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FunBooksAndVideos.Service.Queries
{
    public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQuery, IEnumerable<OrderEntity>>
    {
        private readonly IGenericRepository<OrderEntity> _context;

        public GetAllOrdersQueryHandler(IGenericRepository<OrderEntity> context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OrderEntity>> Handle(GetAllOrdersQuery query, CancellationToken cancellationToken)
        {
            return await _context.GetAll().ToListAsync();
        }
    }
}