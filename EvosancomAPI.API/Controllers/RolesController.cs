using EvosancomAPI.Application.Features.AppRole.Commands.CreateRole;
using EvosancomAPI.Application.Features.AppRole.Commands.DeleteRole;
using EvosancomAPI.Application.Features.AppRole.Commands.UpdateRole;
using EvosancomAPI.Application.Features.AppRole.Queries.GetAllRoles;
using EvosancomAPI.Application.Features.AppRole.Queries.GetRoleById;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EvosancomAPI.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RolesController : ControllerBase
	{
		readonly IMediator _mediator;
		public RolesController(IMediator mediator)
		{
			_mediator = mediator;
		}
		[HttpGet]
		public async Task<IActionResult> GetRoles([FromQuery]GetAllRolesQueryRequest 
			getAllRolesQueryRequest)
		{
			GetAllRolesQueryResponse getAllRolesQueryResponse
				=await _mediator.Send(getAllRolesQueryRequest);
			return Ok(getAllRolesQueryResponse);
		}
		[HttpGet("{Id}")]
		public async Task<IActionResult> GetRolesById([FromRoute]GetRoleByIdQueryRequest getRoleByIdQueryRequest)
		{
			GetRoleByIdQueryResponse getRoleByIdQueryResponse = 
				await _mediator.Send(getRoleByIdQueryRequest);
			return Ok(getRoleByIdQueryResponse);
		}

		[HttpPost()]
		public async Task<IActionResult> CreateRole([FromBody]CreateRoleCommandRequest createRoleCommandRequest)
		{
			CreateRoleCommandResponse createRoleCommandResponse = await
				_mediator.Send(createRoleCommandRequest);
			return Ok(createRoleCommandResponse);
		}

		[HttpPut("{Id}")]
		public async Task<IActionResult> UpdateRole([FromRoute] string Id, [FromBody] UpdateRoleCommandRequest updateRoleCommandRequest)
		{
			updateRoleCommandRequest.Id = Id;
			var response = await _mediator.Send(updateRoleCommandRequest);
			return Ok(response);
		}

		[HttpDelete("{Id}")]
		public async Task<IActionResult> DeleteRole([FromRoute] DeleteRoleCommandRequest deleteRoleCommandRequest)
		{
			DeleteRoleCommandResponse deleteRoleCommandResponse =await
				_mediator.Send(deleteRoleCommandRequest);
			return Ok(deleteRoleCommandResponse);
		}
	}
}
