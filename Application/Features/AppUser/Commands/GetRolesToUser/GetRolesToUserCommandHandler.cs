using EvosancomAPI.Application.Abstractions.Services;
using MediatR;

namespace EvosancomAPI.Application.Features.AppUser.Commands.GetRolesToUser
{
	public class GetRolesToUserCommandHandler : IRequestHandler<GetRolesToUserCommandRequest, GetRolesToUserCommandResponse>
	{
		readonly IUserService _userService;

		public GetRolesToUserCommandHandler(IUserService userService)
		{
			_userService = userService;
		}

		public async Task<GetRolesToUserCommandResponse> Handle(GetRolesToUserCommandRequest request, CancellationToken cancellationToken)
		{
			var userRoles = await _userService.GetRolesToUserAsync(request.UserId);
			return new()
			{
				UserRoles = userRoles
			};
		}
	}

}
