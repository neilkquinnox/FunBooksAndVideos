using FunBooksAndVideos.Infrastructure.Entities;
using MediatR;

namespace FunBooksAndVideos.Service.Queries
{
    public class GetOrderByIdQuery : IRequest<OrderEntity>
    {
        public int Id { get; set; }
    }
}