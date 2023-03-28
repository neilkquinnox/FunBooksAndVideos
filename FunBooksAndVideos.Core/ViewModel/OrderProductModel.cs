namespace FunBooksAndVideos.WebApi.Core.ViewModel
{
    public class OrderProductModel
    {
        public string Product_ID { get; set; }
        public string MembershipName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}