using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Products.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand>
    {
        private readonly IProductRepository _productRepository;

        public CreateProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public Task<Unit> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            Product product = new Product
            {
                Name = request.Name,
                Price = request.Price,
                Description = request.Description
            };

            _productRepository.CreateDapper(product);

            return Unit.Task;
        }
    }
}
