using FunBooksAndVideos.Infrastructure.Repository;

namespace FunBooksAndVideos.Infrastructure.Entities
{
    public class OrderProductEntity : IEntity
    {
        public string Id { get; set; }
        public string Order_ID { get; set; }
        public string Product_ID { get; set; }
        public decimal price { get; set; }
        public string MembershipName { get; set; }
        public int Quantity { get; set; }
        public OrderEntity Order { get; set; }
    }
}