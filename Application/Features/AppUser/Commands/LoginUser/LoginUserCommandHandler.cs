using EvosancomAPI.Application.Abstractions.Services;
using EvosancomAPI.Application.Abstractions.Token;
using EvosancomAPI.Application.DTOs;
using EvosancomAPI.Application.Exceptions;
using EvosancomAPI.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace EvosancomAPI.Application.Features.AppUser.Commands.LoginUser
{
	public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
	{
	
		readonly IAuthService _authService;

		public LoginUserCommandHandler(IAuthService authService)
		{
			_authService = authService;
		}

		public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
		{
	
			var token = await _authService.LoginAsync(
				request.UsernameOrEmail, request.Password, 900);
			return new LoginUserCommandResponse
			{
				Token = token
			};
		}
	}
}
