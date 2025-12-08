using EvosancomAPI.Application.Features.Products.Commands.CreateProduct;
using EvosancomAPI.Application.Features.Products.Commands.DeleteProduct;
using EvosancomAPI.Application.Features.Products.Commands.UpdateProduct;
using EvosancomAPI.Application.Features.Products.Queries.GetProducts;
using EvosancomAPI.Application.Features.Products.Queries.GetProductsById;
using EvosancomAPI.Application.Repositories;
using EvosancomAPI.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EvosancomAPI.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class ProductsController : ControllerBase
	{

		readonly IMediator _mediator;

		public ProductsController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllProducts([FromQuery] GetProductsQueryRequest getProductsQueryRequest)
		{
			GetProductsQueryResponse response= await _mediator.Send(getProductsQueryRequest);
			return Ok(response);
		}

		[HttpGet("{Id}")]
		public async Task<IActionResult> GetProductById([FromRoute] GetProductByIdQueryRequest request)
		{
			GetProductByIdQueryResponse getProductByIdQueryResponse = await _mediator.Send(request);
			return Ok(getProductByIdQueryResponse);
		}

		[HttpPost]
		public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommandRequest createProductCommandRequest)
		{
			CreateProductCommandResponse response = await _mediator.Send(createProductCommandRequest);
			return StatusCode((int)HttpStatusCode.Created);
		}

		[HttpPut] 
		public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductCommandRequest updateProductCommandRequest)
		{
			UpdateProductCommandResponse updateProductCommandResponse = await _mediator.Send(updateProductCommandRequest);
			return Ok();
		}

		[HttpDelete]
		public async Task<IActionResult> DeleteProduct([FromQuery] DeleteProductCommandRequest deleteProductCommandRequest)
		{
			DeleteProductCommandResponse response = await _mediator.Send(deleteProductCommandRequest);
			return Ok();
		}

		[HttpGet("test-yap")]
		[AllowAnonymous] // Login olmadan da çalışsın
		public IActionResult Deneme()
		{
			return Ok("Buradayım, çalışıyorum!");
		}
		//aecd3dff-e079-434f-b190-1e980840ee41
	}
}
