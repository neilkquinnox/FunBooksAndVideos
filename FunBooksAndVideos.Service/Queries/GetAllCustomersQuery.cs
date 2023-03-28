using FunBooksAndVideos.Infrastructure.Entities;
using MediatR;
using System.Collections.Generic;

namespace FunBooksAndVideos.Service.Queries
{
    public class GetAllCustomersQuery : IRequest<IEnumerable<CustomerEntity>>
    {
    }
}