using FunBooksAndVideos.Infrastructure.Entities;
using FunBooksAndVideos.Infrastructure.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FunBooksAndVideos.Service.Commands
{
    public class DeleteMembershipByIdCommandHandler : IRequestHandler<DeleteMembershipByIdCommand, int>
    {
        private readonly IGenericRepository<MembershipEntity> _context;

        public DeleteMembershipByIdCommandHandler(IGenericRepository<MembershipEntity> context)
        {
            _context = context;
        }

        public async Task<int> Handle(DeleteMembershipByIdCommand command, CancellationToken cancellationToken)
        {
            await _context.Delete(command.Id);
            return command.Id;
        }
    }
}