using FunBooksAndVideos.Infrastructure.Entities;
using FunBooksAndVideos.Infrastructure.Repository;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FunBooksAndVideos.Service.Commands
{
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, int>
    {
        private readonly IGenericRepository<CustomerEntity> _context;

        public UpdateCustomerCommandHandler(IGenericRepository<CustomerEntity> context)
        {
            _context = context;
        }

        public async Task<int> Handle(UpdateCustomerCommand command, CancellationToken cancellationToken)
        {
            var customerObj = new CustomerEntity();
            customerObj.Name = command.Name;
            customerObj.Email = command.Email;
            customerObj.Phone = command.Phone;
            customerObj.Id = command.Id.ToString();

            await _context.Update(command.Id, customerObj);
            return Convert.ToInt32(customerObj.Id);
        }
    }
}