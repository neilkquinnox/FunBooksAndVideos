using FunBooksAndVideos.Infrastructure.Entities;
using MediatR;
using System.Collections.Generic;

namespace FunBooksAndVideos.Service.Queries
{
    public class GetAllOrdersQuery : IRequest<IEnumerable<OrderEntity>>
    {
    }
}