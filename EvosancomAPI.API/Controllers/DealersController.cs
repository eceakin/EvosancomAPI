using EvosancomAPI.Application.Features.Dealers.Commands.CreateDealer;
using EvosancomAPI.Application.Features.Dealers.Commands.DeleteDealer;
using EvosancomAPI.Application.Features.Dealers.Commands.UpdateDealer;
using EvosancomAPI.Application.Features.Dealers.Queries.GetAllDealers;
using EvosancomAPI.Application.Features.Dealers.Queries.GetProductsForDealer;
using EvosancomAPI.Application.Features.ProductCategories.Commands.DeleteProductCategory;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EvosancomAPI.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DealersController : ControllerBase
	{
		readonly IMediator _mediator;

		public DealersController(IMediator mediator)
		{
			_mediator = mediator;
		}
		[HttpGet]
		public async Task<IActionResult> GetAllDealers()
		{
			GetAllDealersQueryResponse getAllDealersQueryResponse = await _mediator.Send(new GetAllDealersQueryRequest());
			return Ok(getAllDealersQueryResponse);
		}
		[HttpPost]
		public async Task<IActionResult> CreateDealer([FromBody] CreateDealerCommandRequest request)
		{
			CreateDealerCommandResponse response = await _mediator.Send(request);
			return StatusCode(201, response);
		}

		[HttpPut]
		public async Task<IActionResult> UpdateDealer([FromBody] UpdateDealerCommandRequest request)
		{
			UpdateDealerCommandResponse response = await _mediator.Send(request);
			return Ok(response);
		}

		[HttpDelete("{Id}")]
		public async Task<IActionResult> RemoveDealer([FromRoute] DeleteDealerCommandRequest request)
		{
			DeleteDealerCommandResponse response = await _mediator.Send(request);
			return Ok(response);
		}

		[HttpGet("products")]
		public async Task<IActionResult> GetDealerProducts()
		{
			// Token'dan User ID'yi çekiyoruz
			var userId = User.FindFirstValue(ClaimTypes.Name);

			// Eğer ClaimTypes.NameIdentifier boş geliyorsa Token konfigürasyonuna bakmak gerekebilir
			// Alternatif: User.Identity.Name (Eğer username ile eşleşiyorsa logic değişebilir)

			var request = new GetProductsForDealerQueryRequest
			{
				UserId = userId
			};

			var response = await _mediator.Send(request);
			return Ok(response);
		}
	}

}
