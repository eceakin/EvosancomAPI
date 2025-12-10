using EvosancomAPI.Application.CustomAttributes;
using EvosancomAPI.Application.Features.AppUser.Commands.AssignRoleToUser;
using EvosancomAPI.Application.Features.AppUser.Commands.CreateUser;
using EvosancomAPI.Application.Features.AppUser.Commands.GetRolesToUser;
using EvosancomAPI.Application.Features.AppUser.Commands.LoginUser;
using EvosancomAPI.Application.Features.AppUser.Queries.GetAllUsers;
using EvosancomAPI.Application.Features.AuthorizationEndpoints.Commands.AssignRoleEndpoint;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace EvosancomAPI.API.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		readonly IMediator _mediator;

		public UsersController(IMediator mediator)
		{
			_mediator = mediator;
		}
		
		[HttpPost]
		public async Task<IActionResult> CreateUser([FromBody] CreateUserCommandRequest request)
		{
			CreateUserCommandResponse response = await _mediator.Send(request);
			if (response.Succeeded)
			{
				return Ok(response.Message);
			}
			return BadRequest(response.Message);
		}

		// tüm kullanıcıları bize geri döndürecek 
		[HttpGet]
		[Authorize]
		[AuthorizeDefinition(Menu = "Users", ActionType = Application.Enums.ActionType.Reading, Definition = "Get All Users")]
		public async Task<IActionResult> GetAllUsers([FromQuery] GetAllUsersQueryRequest request)
		{
			GetAllUsersQueryResponse getAllUsersQueryResponse = await _mediator.Send(request);
			return Ok(getAllUsersQueryResponse);
		}

		[HttpPost("assign-role-to-user")]
		[Authorize]
		[AuthorizeDefinition(Menu = "Users", ActionType = Application.Enums.ActionType.Writing, Definition = "Assign Role To User")]
		public async Task<IActionResult> AssignRoleToUser(AssignRoleToUserCommandRequest request)
		{
			AssignRoleToUserCommandResponse assignRoleToUserCommandResponse = await _mediator.Send(request);
			return Ok(assignRoleToUserCommandResponse);
		}

		[HttpGet("get-roles-to-user/{UserId}")]
		[Authorize]
		[AuthorizeDefinition(Menu = "Users", ActionType = Application.Enums.ActionType.Reading, Definition = "Get roles to User")]
		public async Task<IActionResult> GetRolesToUser([FromRoute] GetRolesToUserCommandRequest request)
		{
			GetRolesToUserCommandResponse getRolesToUserCommandResponse = await _mediator.Send(request);
			return Ok(getRolesToUserCommandResponse);
		}

		//3a58228c-9487-4bc3-b9d2-17e80133cb3a
	}
}
