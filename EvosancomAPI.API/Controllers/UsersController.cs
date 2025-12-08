using EvosancomAPI.Application.Features.AppUser.Commands.CreateUser;
using EvosancomAPI.Application.Features.AppUser.Commands.LoginUser;
using MediatR;
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

	
	}
}
