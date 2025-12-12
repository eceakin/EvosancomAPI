using EvosancomAPI.Application.Commons.Models;
using EvosancomAPI.Application.Features.Products.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvosancomAPI.Application.Features.Products.Commands.UpdateProduct
{

	public class UpdateProductCommandRequest : IRequest<UpdateProductCommandResponse>
	{
		public string Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public string Code { get; set; } = string.Empty;
		public string? Barcode { get; set; }
		public Guid ProductCategoryId { get; set; }
		public string? Description { get; set; }
        public int StockQuantity { get; set; }
        public decimal BasePrice { get; set; }
		public bool IsCustomizable { get; set; }
		public string? ImageUrl { get; set; }
		public bool IsActive { get; set; }
		public string? Specifications { get; set; }
	}
}
