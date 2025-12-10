using EvosancomAPI.Application.Features.AuthorizationEndpoints.Commands.AssignRoleEndpoint;
using EvosancomAPI.Application.Features.AuthorizationEndpoints.Queries.GetRolesToEndpoints;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EvosancomAPI.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthorizationEndpointsController : ControllerBase
	{
		readonly IMediator _mediator;
		public AuthorizationEndpointsController(IMediator mediator)
		{
			_mediator = mediator;
		}
		[HttpPost]
		public async Task<IActionResult> AssignRoleEndpoint(AssignRoleEndpointCommandRequest assignRoleEndpointCommandRequest)
		{
			assignRoleEndpointCommandRequest.Type = typeof(Program);
			AssignRoleEndpointCommandResponse assignRoleEndpointCommandResponse = await _mediator.
				Send(assignRoleEndpointCommandRequest);
			return Ok(assignRoleEndpointCommandResponse);

		}

		[HttpPost("get-roles-to-endpoint")]
		public async Task<IActionResult> GetRolesToEndpoint([FromBody]
		GetRolesToEndpointQueryRequest request)
		{
			GetRolesToEndpointQueryResponse response = await _mediator.Send(request);
			return Ok(response);
		}
	}
}
