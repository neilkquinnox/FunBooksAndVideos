using MediatR;

namespace FunBooksAndVideos.Service.Commands
{
    public class UpdateMembershipCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
    }
}