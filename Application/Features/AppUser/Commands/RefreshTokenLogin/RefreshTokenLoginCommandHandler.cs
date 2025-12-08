using EvosancomAPI.Application.Abstractions.Services;
using EvosancomAPI.Application.DTOs;
using MediatR;

namespace EvosancomAPI.Application.Features.AppUser.Commands.RefreshTokenLogin
{
	public class RefreshTokenLoginCommandHandler:IRequestHandler<RefreshTokenLoginCommandRequest, RefreshTokenLoginCommandResponse>
	{
		readonly IAuthService _authService;

		public RefreshTokenLoginCommandHandler(IAuthService authService)
		{
			_authService = authService;
		}

		public async Task<RefreshTokenLoginCommandResponse> Handle(RefreshTokenLoginCommandRequest request, CancellationToken cancellationToken)
		{
			Token token = await _authService.RefreshTokenLoginAsync(request.RefreshToken);
			return new()
			{
				Token = token
			};

		}
	}
}
