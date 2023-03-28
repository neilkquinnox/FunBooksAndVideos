using FunBooksAndVideos.Infrastructure.Entities;
using FunBooksAndVideos.Infrastructure.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FunBooksAndVideos.Service.Commands
{
    public class DeleteCustomerByIdCommandHandler : IRequestHandler<DeleteCustomerByIdCommand, int>
    {
        private readonly IGenericRepository<CustomerEntity> _context;

        public DeleteCustomerByIdCommandHandler(IGenericRepository<CustomerEntity> context)
        {
            _context = context;
        }

        public async Task<int> Handle(DeleteCustomerByIdCommand command, CancellationToken cancellationToken)
        {
            await _context.Delete(command.Id);
            return command.Id;
        }
    }
}