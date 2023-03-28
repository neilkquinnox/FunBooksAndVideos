using FunBooksAndVideos.Infrastructure.Repository;
using System;
using System.Collections.Generic;

namespace FunBooksAndVideos.Infrastructure.Entities
{
    public class OrderEntity : IEntity
    {
        public string Id { get; set; }
        public string Customer_ID { get; set; }
        public DateTime Date { get; set; }
        public decimal price { get; set; }
        public List<OrderProductEntity> orderProducts { get; set; }
    }
}