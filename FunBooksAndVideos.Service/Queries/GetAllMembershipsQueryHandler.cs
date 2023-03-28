using FunBooksAndVideos.Infrastructure.Entities;
using FunBooksAndVideos.Infrastructure.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FunBooksAndVideos.Service.Queries
{
    public class GetAllMembershipsQueryHandler : IRequestHandler<GetAllMembershipsQuery, IEnumerable<MembershipEntity>>
    {
        private readonly IGenericRepository<MembershipEntity> _context;

        public GetAllMembershipsQueryHandler(IGenericRepository<MembershipEntity> context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MembershipEntity>> Handle(GetAllMembershipsQuery query, CancellationToken cancellationToken)
        {
            return await _context.GetAll().ToListAsync();
        }
    }
}