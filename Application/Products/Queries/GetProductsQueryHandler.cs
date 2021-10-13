using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Products.Queries
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, List<Product>>
    {
        private readonly IProductRepository _productRepository;

        public GetProductsQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public Task<List<Product>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_productRepository.GetProductsDapper());
        }
    }
}
