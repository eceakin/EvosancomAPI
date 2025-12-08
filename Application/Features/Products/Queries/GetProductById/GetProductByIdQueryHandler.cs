using AutoMapper;
using EvosancomAPI.Application.Commons.Interfaces;
using EvosancomAPI.Application.Commons.Models;
using EvosancomAPI.Application.Features.Products.DTOs;
using EvosancomAPI.Application.Features.Products.Queries.GetProductsById;
using EvosancomAPI.Application.Repositories;
using EvosancomAPI.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EvosancomAPI.Application.Features.Products.Queries.GetProductById
{
	public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQueryRequest,GetProductByIdQueryResponse>
	{
		readonly IProductReadRepository _productReadRepository;

		public GetProductByIdQueryHandler(IProductReadRepository productReadRepository)
		{
			_productReadRepository = productReadRepository;
		}
		public async Task<GetProductByIdQueryResponse> Handle(GetProductByIdQueryRequest request, CancellationToken cancellationToken)
		{
			Product product = await _productReadRepository.GetByIdAsync(request.Id, false);
			return new()
			{
				Name = product.Name,
				Description = product.Description
			};

		}
	}
}
