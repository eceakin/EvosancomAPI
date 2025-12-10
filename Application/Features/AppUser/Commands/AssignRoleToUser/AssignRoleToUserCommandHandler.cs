using EvosancomAPI.Application.Abstractions.Services;
using MediatR;

namespace EvosancomAPI.Application.Features.AppUser.Commands.AssignRoleToUser
{
	public class AssignRoleToUserCommandHandler :
		IRequestHandler<AssignRoleToUserCommandRequest, AssignRoleToUserCommandResponse>
	{
		readonly IUserService _userService;

		public AssignRoleToUserCommandHandler(IUserService userService)
		{
			_userService = userService;
		}

		public async Task<AssignRoleToUserCommandResponse> Handle(AssignRoleToUserCommandRequest request, CancellationToken cancellationToken)
		{
			await _userService.AssingRoleToUserAsync(request.UserId, request.Roles);
			return new();
		}
	}
}
