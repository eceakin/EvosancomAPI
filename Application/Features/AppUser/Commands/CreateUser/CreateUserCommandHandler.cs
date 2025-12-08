using EvosancomAPI.Application.Abstractions.Services;
using EvosancomAPI.Application.DTOs.User;
using EvosancomAPI.Application.Exceptions;
using EvosancomAPI.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace EvosancomAPI.Application.Features.AppUser.Commands.CreateUser
{
	public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
	{
		readonly UserManager<ApplicationUser> _userManager;
		readonly IUserService _userService;
		public CreateUserCommandHandler(UserManager<ApplicationUser> userManager, IUserService userService)
		{
			_userManager = userManager;
			_userService = userService;
		}

		public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
		{
			CreateUserResponseDto responseDto =await _userService.CreateAsync(new()
			{
				UserName = request.UserName,
				FirstName = request.FirstName,
				LastName = request.LastName,
				Email = request.Email,
				Password = request.Password,
			});

			return new()
			{
				Message = responseDto.Message,
				Succeeded = responseDto.Succeeded
			};


		}
	}
}
