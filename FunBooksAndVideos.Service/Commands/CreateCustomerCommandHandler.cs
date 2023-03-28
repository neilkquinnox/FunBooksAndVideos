using FunBooksAndVideos.Infrastructure.Entities;
using FunBooksAndVideos.Infrastructure.Repository;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FunBooksAndVideos.Service.Commands
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, int>
    {
        private readonly IGenericRepository<CustomerEntity> _context;

        public CreateCustomerCommandHandler(IGenericRepository<CustomerEntity> context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateCustomerCommand command, CancellationToken cancellationToken)
        {
            var Customer = new CustomerEntity();
            Customer.Id = _context.GetAll().Count() == 0 ? "0" : Convert.ToString(Convert.ToInt32(_context.GetAll().Max(x => x.Id)) + 1);
            Customer.Name = command.Name;
            Customer.Email = command.Email;
            Customer.Phone = command.Phone;
            await _context.Create(Customer);
            return Convert.ToInt32(Customer.Id);
        }
    }
}