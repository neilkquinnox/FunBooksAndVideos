using FunBooksAndVideos.Infrastructure.Entities;
using FunBooksAndVideos.Infrastructure.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FunBooksAndVideos.Service.Queries
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<ProductEntity>>
    {
        private readonly IGenericRepository<ProductEntity> _context;

        public GetAllProductsQueryHandler(IGenericRepository<ProductEntity> context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductEntity>> Handle(GetAllProductsQuery query, CancellationToken cancellationToken)
        {
            return await _context.GetAll().ToListAsync();
        }
    }
}