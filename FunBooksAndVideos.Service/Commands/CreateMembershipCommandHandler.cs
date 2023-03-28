using FunBooksAndVideos.Infrastructure.Entities;
using FunBooksAndVideos.Infrastructure.Repository;
using MediatR;
using System;
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
            var Membership = new MembershipEntity();
            Membership.Name = command.Name;
            Membership.Description = command.Description;
            Membership.Price = command.Price;
            await _context.Create(Membership);
            return Convert.ToInt32(Membership.Id);
        }
    }
}