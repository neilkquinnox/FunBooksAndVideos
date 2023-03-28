using AutoMapper;
using FunBooksAndVideos.Core.Models;
using FunBooksAndVideos.Infrastructure.Entities;
using FunBooksAndVideos.Infrastructure.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FunBooksAndVideos.Service.Queries
{
    public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, CustomerModel>
    {
        private readonly IGenericRepository<CustomerEntity> _context;
        private readonly IMapper _mapper;

        public GetCustomerByIdQueryHandler(IGenericRepository<CustomerEntity> context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CustomerModel> Handle(GetCustomerByIdQuery query, CancellationToken cancellationToken)
        {
            var customer = await _context.GetById(query.Id);
            if (customer == null)
            {
                return null;
            }

            var customerDTO = _mapper.Map<CustomerModel>(customer);
            return customerDTO;
        }
    }
}