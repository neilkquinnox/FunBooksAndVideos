using FunBooksAndVideos.Infrastructure.Repository;

namespace FunBooksAndVideos.Infrastructure.Entities
{
    public class ProductEntity : IEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Barcode { get; set; }
        public bool IsActive { get; set; }
        public string Description { get; set; }
        public decimal price { get; set; }
        public string Category { get; set; }
    }
}