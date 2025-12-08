using EvosancomAPI.Application.Features.ProductCategories.Commands.CreateProductCategory;
using EvosancomAPI.Application.Features.ProductCategories.Commands.DeleteProductCategory;
using EvosancomAPI.Application.Features.ProductCategories.Commands.UpdateProductCategory;
using EvosancomAPI.Application.Features.ProductCategories.Queries.GetAllProductCategories;
using EvosancomAPI.Application.Features.ProductCategories.Queries.GetProductCategoryById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EvosancomAPI.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductCategoriesController : ControllerBase
	{
		readonly IMediator _mediator;
		public ProductCategoriesController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllProductCategories([FromQuery] GetAllProductCategoriesQueryRequest getProductCategoriesQueryRequest)
		{
			GetAllProductCategoriesQueryResponse response = await _mediator.Send(getProductCategoriesQueryRequest);
			return Ok(response);
		}

		[HttpGet("{Id}")]
		public async Task<IActionResult> GetProductCategoryById([FromRoute] GetProductCategoryByIdQueryRequest request)
		{
			GetProductCategoryByIdQueryResponse response = await _mediator.Send(request);
			return Ok(response);
		}

		[HttpPost]
		public async Task<IActionResult> CreateProductCategory([FromBody] CreateProductCategoryCommandRequest createProductCategoryCommandRequest)
		{
			CreateProductCategoryCommandResponse response = await _mediator.Send(createProductCategoryCommandRequest);
			return StatusCode(StatusCodes.Status201Created);
		}
		[HttpPut]
		public async Task<IActionResult> UpdateProductCategory([FromBody] UpdateProductCategoryCommandRequest updateProductCategoryCommandRequest)
		{
			UpdateProductCategoryCommandResponse response = await _mediator.Send(updateProductCategoryCommandRequest);
			return Ok();
		}

		[HttpDelete]
		public async Task<IActionResult> DeleteProductCategory([FromQuery] DeleteProductCategoryCommandRequest deleteProductCategoryCommandRequest)
		{
			DeleteProductCategoryCommandResponse response = await _mediator.Send(deleteProductCategoryCommandRequest);
			return Ok();
		}
		[HttpGet("test-yap")]
		[AllowAnonymous] // Login olmadan da çalışsın
		public IActionResult Deneme()
		{
			return Ok("Buradayım, çalışıyorum!");
		}
	}
}
