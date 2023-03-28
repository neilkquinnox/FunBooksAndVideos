using FunBooksAndVideos.WebApi.Core.ViewModel;
using MediatR;
using System;
using System.Collections.Generic;

namespace FunBooksAndVideos.Service.Commands
{
    public class CreateOrderCommand : IRequest<string>
    {
        public string Customer_ID { get; set; }
        public DateTime Date { get; set; }
        public List<OrderProductModel> orderProducts { get; set; }
    }
}