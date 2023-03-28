using MediatR;

namespace FunBooksAndVideos.Service.Commands
{
    public class DeleteOrderByIdCommand : IRequest<string>
    {
        public int Id { get; set; }
    }
}