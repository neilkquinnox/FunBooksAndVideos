using FunBooksAndVideos.Infrastructure.Entities;
using FunBooksAndVideos.Infrastructure.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FunBooksAndVideos.Service.Queries
{
    public class GetMembershipByIdQueryHandler : IRequestHandler<GetMembershipByIdQuery, MembershipEntity>
    {
        private readonly IGenericRepository<MembershipEntity> _context;

        public GetMembershipByIdQueryHandler(IGenericRepository<MembershipEntity> context)
        {
            _context = context;
        }

        public async Task<MembershipEntity> Handle(GetMembershipByIdQuery query, CancellationToken cancellationToken)
        {
            return await _context.GetById(query.Id);
        }
    }
}