using MediatR;

namespace FunBooksAndVideos.Service.Queries
{
    public class GetCustomerByIdQuery : IRequest<Core.Models.CustomerModel>
    {
        public int Id { get; set; }
    }
}