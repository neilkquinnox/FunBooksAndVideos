using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcomShop.WebApi.Domain.Models
{
    public class OrderProductVM
    {     
        public string Product_ID { get; set; }
        public string MembershipName { get; set; }
        public int Quantity  { get; set; }
        public decimal price { get; set; }
    }
}
