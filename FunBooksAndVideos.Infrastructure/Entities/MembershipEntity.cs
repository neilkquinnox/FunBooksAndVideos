using FunBooksAndVideos.Infrastructure.Repository;

namespace FunBooksAndVideos.Infrastructure.Entities
{
    public class MembershipEntity : IEntity
    {
        public string Id { get; set; }
        public bool IsActive { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
    }
}