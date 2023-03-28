using MediatR;

namespace FunBooksAndVideos.Service.Commands
{
    public class DeleteProductByIdCommand : IRequest<string>
    {
        public int Id { get; set; }
    }
}