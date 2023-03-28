using FunBooksAndVideos.Infrastructure.Entities;
using FunBooksAndVideos.Infrastructure.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FunBooksAndVideos.Service.Queries
{
    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OrderEntity>
    {
        private readonly IGenericRepository<OrderEntity> _context;

        public GetOrderByIdQueryHandler(IGenericRepository<OrderEntity> context)
        {
            _context = context;
        }

        public async Task<OrderEntity> Handle(GetOrderByIdQuery query, CancellationToken cancellationToken)
        {
            var order = await _context.GetById(query.Id);
            return order;
        }
    }
}