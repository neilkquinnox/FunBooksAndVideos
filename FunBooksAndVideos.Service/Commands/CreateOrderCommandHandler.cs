using FunBooksAndVideos.Infrastructure.Entities;
using FunBooksAndVideos.Infrastructure.Repository;

using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FunBooksAndVideos.Service.Commands
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, string>
    {
        private readonly IGenericRepository<OrderEntity> _orderContext;
        private readonly IGenericRepository<OrderProductEntity> _orderProductContext;
        private readonly IGenericRepository<CustomerMembershipEntity> _customerMemberContext;

        public CreateOrderCommandHandler(IGenericRepository<OrderEntity> orderContext, IGenericRepository<OrderProductEntity> orderProductContext, IGenericRepository<CustomerMembershipEntity> customerMemberContext)
        {
            _orderContext = orderContext;
            _orderProductContext = orderProductContext;
            _customerMemberContext = customerMemberContext;
        }

        public async Task<string> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
        {
            var Order = new OrderEntity();

            Order.Id = _orderContext.GetAll().Count() == 0 ? "0" : Convert.ToString(Convert.ToInt32(_orderContext.GetAll().Max(x => x.Id)) + 1);
            Order.Customer_ID = command.Customer_ID.ToString();
            Order.Date = command.Date;
            await _orderContext.Create(Order);

            foreach (var orderitem in command.orderProducts)
            {
                var orderdetail = new OrderProductEntity();
                {
                    orderdetail.Order = Order;
                    orderdetail.Order_ID = Order.Id.ToString();
                    orderdetail.Product_ID = orderitem.Product_ID;
                    orderdetail.MembershipName = orderitem.MembershipName;
                    orderdetail.Quantity = orderitem.Quantity;
                    orderdetail.price = orderitem.Price;
                }
                await _orderProductContext.Create(orderdetail);

                if (!string.IsNullOrEmpty(orderitem.MembershipName))
                {
                    var CustomerMembership = new CustomerMembershipEntity();
                    CustomerMembership.CustomerId = command.Customer_ID;
                    await _customerMemberContext.Create(CustomerMembership);
                }
            }
            return Order.Id.ToString();
        }
    }
}