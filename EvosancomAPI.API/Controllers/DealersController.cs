using EvosancomAPI.Application.Features.Dealers.Commands.CreateDealer;
using EvosancomAPI.Application.Features.Dealers.Commands.UpdateDealer;
using EvosancomAPI.Application.Features.Dealers.Queries.GetAllDealers;
using EvosancomAPI.Application.Features.Dealers.Queries.GetDealerById;
using EvosancomAPI.Application.Features.ProductCategories.Commands.DeleteProductCategory;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace EvosancomAPI.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DealersController : ControllerBase
	{/*
		readonly IMediator _mediator;

		public DealersController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetDealerById([FromRoute] GetDealerByIdQueryRequest getDealerByIdQueryRequest)
		{
			GetDealerByIdQueryResponse getDealerByIdQueryResponse = await _mediator
				.Send(getDealerByIdQueryRequest);
			return Ok(getDealerByIdQueryResponse);
		}

		[HttpGet]
		public async Task<IActionResult> GetAllDealers([FromQuery] GetAllDealersQueryRequest getAllDealersQueryRequest)
		{
			GetAllDealersQueryResponse getAllDealersQueryResponse = await _mediator
				.Send(getAllDealersQueryRequest);
			return Ok(getAllDealersQueryResponse);

		}

		[HttpPost]
		public async Task<IActionResult> CreateDealer([FromBody] CreateDealerCommandRequest createDealerCommandRequest)
		{
			CreateDealerCommandResponse response = await _mediator
				.Send(createDealerCommandRequest);
			return Ok(response);
		}
		[HttpPut]
		public async Task<IActionResult> UpdateDealer([FromBody] UpdateDealerCommandRequest updateDealerCommandRequest)
		{
			UpdateDealerCommandResponse response = await _mediator
				.Send(updateDealerCommandRequest);
			return Ok(response);
		}

		[HttpDelete]
		public async Task<IActionResult> DeleteDealer([FromQuery] DeleteProductCategoryCommandRequest deleteProductCategoryCommandRequest)
		{
			DeleteProductCategoryCommandResponse response = await _mediator
				.Send(deleteProductCategoryCommandRequest);
			return Ok(response);
		}
			}*/
	}

}
