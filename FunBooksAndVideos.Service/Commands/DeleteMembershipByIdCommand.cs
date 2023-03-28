using MediatR;

namespace FunBooksAndVideos.Service.Commands
{
    public class DeleteMembershipByIdCommand : IRequest<int>
    {
        public int Id { get; set; }
    }
}