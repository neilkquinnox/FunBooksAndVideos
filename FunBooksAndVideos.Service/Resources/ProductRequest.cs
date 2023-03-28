namespace FunBooksAndVideos.Service.Resources
{
    public class ProductRequest
    {
        public string Name { get; set; }
        public string Barcode { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public decimal price { get; set; }
    }
}