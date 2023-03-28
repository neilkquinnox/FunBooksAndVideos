using FunBooksAndVideos.Infrastructure.Entities;
using MediatR;

namespace FunBooksAndVideos.Service.Queries
{
    public class GetMembershipByIdQuery : IRequest<MembershipEntity>
    {
        public int Id { get; set; }
    }
}