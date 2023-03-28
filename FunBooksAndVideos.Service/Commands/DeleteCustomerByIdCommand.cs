using MediatR;

namespace FunBooksAndVideos.Service.Commands
{
    public class DeleteCustomerByIdCommand : IRequest<int>
    {
        public int Id { get; set; }
    }
}