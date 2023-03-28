using FunBooksAndVideos.Infrastructure.Repository;

namespace FunBooksAndVideos.Infrastructure.Entities
{
    public class CustomerMembershipEntity : IEntity
    {
        public string Id { get; set; }
        public bool IsActive { get; set; }
        public string CustomerId { get; set; }
    }
}