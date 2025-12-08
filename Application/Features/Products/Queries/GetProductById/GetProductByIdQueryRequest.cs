using EvosancomAPI.Application.Commons.Models;
using EvosancomAPI.Application.Features.Products.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvosancomAPI.Application.Features.Products.Queries.GetProductsById
{

	public class GetProductByIdQueryRequest : IRequest<GetProductByIdQueryResponse>
	{
		public string Id { get; set; }
	}
}
