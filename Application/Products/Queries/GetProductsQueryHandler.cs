using AutoMapper;
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
  public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, List<GetProductsResponse>>
  {
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;


    public GetProductsQueryHandler(IProductRepository productRepository, IMapper mapper)
    {
      _productRepository = productRepository;
      _mapper = mapper;
    }

    public Task<List<GetProductsResponse>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
      var products = _productRepository.GetProductsDapper();
      var result = _mapper.Map<List<GetProductsResponse>>(products);
      return Task.FromResult(result);
    }
  }
}
