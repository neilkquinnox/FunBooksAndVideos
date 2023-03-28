using FunBooksAndVideos.Infrastructure.Entities;
using FunBooksAndVideos.Infrastructure.Repository;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FunBooksAndVideos.Service.Commands
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
    {
        private readonly IGenericRepository<ProductEntity> _productContext;

        public CreateProductCommandHandler(IGenericRepository<ProductEntity> productContext)
        {
            _productContext = productContext;
        }

        public async Task<int> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            var product = new ProductEntity();
            product.Id = Convert.ToString(Convert.ToInt32(_productContext.GetAll().Max(x => x.Id)) + 1);
            product.Barcode = command.Barcode;
            product.Name = command.Name;
            product.price = command.Price;
            product.Description = command.Description;
            product.Category = command.Category;
            await _productContext.Create(product);
            return Convert.ToInt32(product.Id);
        }
    }
}