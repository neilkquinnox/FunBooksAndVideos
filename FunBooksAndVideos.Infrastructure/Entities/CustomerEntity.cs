using FunBooksAndVideos.Infrastructure.Repository;

namespace FunBooksAndVideos.Infrastructure.Entities
{
    public class CustomerEntity : IEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public string Phone { get; set; }
    }
}