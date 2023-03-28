using FunBooksAndVideos.WebApi.Core.ViewModel;
using System;
using System.Collections.Generic;

namespace FunBooksAndVideos.Service.Resources
{
    public class OrderRequest
    {
        public string Customer_ID { get; set; }

        public DateTime Date { get; set; }

        public List<OrderProductModel> orderProducts { get; set; }
    }
}