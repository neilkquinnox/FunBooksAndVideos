using FunBooksAndVideos.Infrastructure.Entities;
using FunBooksAndVideos.Infrastructure.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FunBooksAndVideos.Service.Commands
{
    public class DeleteProductByIdCommandHandler : IRequestHandler<DeleteProductByIdCommand, string>
    {
        private readonly IGenericRepository<ProductEntity> _productContext;

        public DeleteProductByIdCommandHandler(IGenericRepository<ProductEntity> productContext)
        {
            _productContext = productContext;
        }

        public async Task<string> Handle(DeleteProductByIdCommand command, CancellationToken cancellationToken)
        {
            await _productContext.Delete(command.Id);
            return command.Id.ToString();
        }
    }
}