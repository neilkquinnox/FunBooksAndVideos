using FunBooksAndVideos.Infrastructure.Entities;
using FunBooksAndVideos.Infrastructure.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FunBooksAndVideos.Service.Commands
{
    public class DeleteOrderByIdCommandHandler : IRequestHandler<DeleteOrderByIdCommand, string>
    {
        private readonly IGenericRepository<OrderEntity> _orderContext;

        public DeleteOrderByIdCommandHandler(IGenericRepository<OrderEntity> orderContext)
        {
            _orderContext = orderContext;
        }

        public async Task<string> Handle(DeleteOrderByIdCommand command, CancellationToken cancellationToken)
        {
            await _orderContext.Delete(command.Id);
            return command.Id.ToString();
        }
    }
}