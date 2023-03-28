using FunBooksAndVideos.Infrastructure.Entities;
using MediatR;

namespace FunBooksAndVideos.Service.Queries
{
    public class GetProductByIdQuery : IRequest<ProductEntity>
    {
        public int Id { get; set; }
    }
}