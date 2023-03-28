using FunBooksAndVideos.Infrastructure.Entities;
using FunBooksAndVideos.Infrastructure.Repository;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FunBooksAndVideos.Service.Commands
{
    public class CreateMembershipCommandHandler : IRequestHandler<CreateMembershipCommand, int>
    {
        private readonly IGenericRepository<MembershipEntity> _context;

        public CreateMembershipCommandHandler(IGenericRepository<MembershipEntity> context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateMembershipCommand command, CancellationToken cancellationToken)
        {
            var membership = new MembershipEntity();
            membership.Id = _context.GetAll().Count() == 0 ? "0" : Convert.ToString(Convert.ToInt32(_context.GetAll().Max(x => x.Id)) + 1);
            membership.Name = command.Name;
            membership.Description = command.Description;
            membership.Price = command.Price;
            await _context.Create(membership);
            return Convert.ToInt32(membership.Id);
        }
    }
}