using EvosancomAPI.Application.Features.AppUser.Commands.LoginUser;
using EvosancomAPI.Application.Features.AppUser.Commands.RefreshTokenLogin;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EvosancomAPI.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		readonly IMediator _mediator;
		public AuthController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpPost("login")]
		public async Task<IActionResult> Login(LoginUserCommandRequest loginUserCommandRequest)
		{
			LoginUserCommandResponse loginUserCommandResponse = await _mediator.Send(loginUserCommandRequest);
			return Ok(loginUserCommandResponse);
		}

		[HttpPost("refresh-token")]

		public async Task<IActionResult> RefreshTokenLogin([FromForm] RefreshTokenLoginCommandRequest request)
		{
			RefreshTokenLoginCommandResponse response = await _mediator.Send(request);
			return Ok(response);

		}
	}
}
