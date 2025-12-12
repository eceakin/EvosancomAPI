using AutoMapper;
using EvosancomAPI.Application.Commons.Interfaces;
using EvosancomAPI.Application.Commons.Models;
using EvosancomAPI.Application.Features.Products.DTOs;
using EvosancomAPI.Application.Repositories;
using EvosancomAPI.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EvosancomAPI.Application.Features.Products.Commands.UpdateProduct
{


	public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest, UpdateProductCommandResponse>
	{
		readonly IProductReadRepository _productReadRepository;
		readonly IProductWriteRepository _productWriteRepository;
		readonly ILogger<UpdateProductCommandHandler> _logger;
		public UpdateProductCommandHandler(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository, ILogger<UpdateProductCommandHandler> logger)
		{
			_productReadRepository = productReadRepository;
			_productWriteRepository = productWriteRepository;
			_logger = logger;
		}

		public async Task<UpdateProductCommandResponse> Handle(UpdateProductCommandRequest request , CancellationToken cancellationToken)
		{
			Product product = await _productReadRepository.GetByIdAsync(request.Id);
			product.Name = request.Name;
			product.Code = request.Code;
			product.Barcode = request.Barcode;
			product.ProductCategoryId = request.ProductCategoryId;
			product.Description = request.Description;
			product.BasePrice = request.BasePrice;
			product.IsCustomizable = request.IsCustomizable;
			product.StockQuantity = request.StockQuantity;
			product.ImageUrl = request.ImageUrl;
			product.IsActive = request.IsActive;
			product.UpdatedDate = DateTime.UtcNow;
			await _productWriteRepository.SaveAsync();
			_logger.LogInformation("Product with name {Name} has been updated.", product.Name);
			return new();
		}
	}
}
