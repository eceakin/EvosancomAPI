using AutoMapper;
using EvosancomAPI.Application.Commons.Interfaces;
using EvosancomAPI.Application.Commons.Models;
using EvosancomAPI.Application.Features.Products.DTOs;
using EvosancomAPI.Application.Repositories;
using EvosancomAPI.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EvosancomAPI.Application.Features.Products.Commands.CreateProduct
{
	public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest, CreateProductCommandResponse>
	{
		private readonly IProductWriteRepository _productWriteRepository;

		public CreateProductCommandHandler(IProductWriteRepository productWriteRepository)
		{
			_productWriteRepository = productWriteRepository;
		}

		public async Task<CreateProductCommandResponse> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
		{
			// Yeni product nesnesi oluştur
			var newProduct = new Product
			{
				Name = request.Name,
				Code = request.Code,
				Barcode = request.Barcode,
				ProductCategoryId = request.ProductCategoryId,
				Description = request.Description,
				BasePrice = request.BasePrice,
				IsCustomizable = request.IsCustomizable,
				ImageUrl = request.ImageUrl
			};

			// Veritabanına ekle
			await _productWriteRepository.AddAsync(newProduct);
			await _productWriteRepository.SaveAsync();

			// Response döndür
			return new CreateProductCommandResponse
			{
				Success = true,
				Message = "Product başarıyla oluşturuldu.",
				CreatedProductId = newProduct.Id
			};
		}
	}

}