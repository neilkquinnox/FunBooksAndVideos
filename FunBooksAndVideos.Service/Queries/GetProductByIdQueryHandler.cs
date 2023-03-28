using FunBooksAndVideos.Infrastructure.Entities;
using FunBooksAndVideos.Infrastructure.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FunBooksAndVideos.Service.Queries
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductEntity>
    {
        private readonly IGenericRepository<ProductEntity> _context;

        public GetProductByIdQueryHandler(IGenericRepository<ProductEntity> context)
        {
            _context = context;
        }

        public async Task<ProductEntity> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            return await _context.GetById(query.Id);
        }
    }
}