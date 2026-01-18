using EvosancomAPI.Application.Features.Orders.Commands.CreateOrder;
using EvosancomAPI.Application.Features.Orders.Queries.GetAllOrders;
using EvosancomAPI.Application.Features.Orders.Queries.GetOrderById;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EvosancomAPI.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OrdersController : ControllerBase
	{
		readonly IMediator _mediator;

		public OrdersController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpPost]
		public async Task<IActionResult> CreateOrder(CreateOrderCommandRequest request)
		{
			CreateOrderCommandResponse response = await _mediator.Send(request);
			return Ok(response);
		}
		[HttpGet]
		public async Task<IActionResult> GetOrders([FromQuery] GetOrdersQueryRequest request)
		{
			var response = await _mediator.Send(request);
			return Ok(response);
		}

		[HttpGet("{Id}")]
		public async Task<IActionResult> GetOrderById([FromRoute] GetOrderByIdQueryRequest request)
		{
			var response = await _mediator.Send(request);
			return Ok(response);
		}
	}
}
