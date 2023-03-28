using AutoMapper;
using FunBooksAndVideos.Core.Models;
using FunBooksAndVideos.Infrastructure.Entities;

namespace FunBooksAndVideos.Service.Customers.DTO
{
    public class CustomerAutoMapperProfile : Profile
    {
        public CustomerAutoMapperProfile()
        {
            CreateMap<CustomerEntity, CustomerModel>();
            CreateMap<CustomerEntity, CustomerModel>();
        }
    }
}