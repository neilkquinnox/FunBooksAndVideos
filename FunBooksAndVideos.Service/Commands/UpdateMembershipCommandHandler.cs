using FunBooksAndVideos.Infrastructure.Entities;
using FunBooksAndVideos.Infrastructure.Repository;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FunBooksAndVideos.Service.Commands
{
    public class UpdateMembershipCommandHandler : IRequestHandler<UpdateMembershipCommand, int>
    {
        private readonly IGenericRepository<MembershipEntity> _context;

        public UpdateMembershipCommandHandler(IGenericRepository<MembershipEntity> context)
        {
            _context = context;
        }

        public async Task<int> Handle(UpdateMembershipCommand command, CancellationToken cancellationToken)
        {
            var membershipObj = new MembershipEntity();
            membershipObj.Name = command.Name;
            membershipObj.Description = command.Description;
            membershipObj.Price = command.Price;
            await _context.Update(command.Id, membershipObj);
            return Convert.ToInt32(membershipObj.Id);
        }
    }
}