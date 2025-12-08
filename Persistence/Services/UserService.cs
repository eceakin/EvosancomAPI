using EvosancomAPI.Application.Abstractions.Services;
using EvosancomAPI.Application.DTOs.User;
using EvosancomAPI.Application.Exceptions;
using EvosancomAPI.Application.Features.AppUser.Commands.CreateUser;
using EvosancomAPI.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvosancomAPI.Persistence.Services
{
	public class UserService : IUserService
	{
		readonly UserManager<ApplicationUser> _userManager;

		public UserService(UserManager<ApplicationUser> userManager)
		{
			_userManager = userManager;
		}

		public async Task<CreateUserResponseDto> CreateAsync(CreateUserDto model)
		{
			

			IdentityResult result = await _userManager.CreateAsync(new ApplicationUser
			{
				Id = Guid.NewGuid().ToString(),
				UserName = model.UserName,
				FirstName = model.FirstName,
				LastName = model.LastName,
				Email = model.Email,
			}, model.Password);

			CreateUserResponseDto response = new() { Succeeded = result.Succeeded };
			if (result.Succeeded)
			{
				response.Message = "User created successfully.";
			}
			else
			{
				foreach (var error in result.Errors)
				{
					response.Message += $"{error.Code} - {error.Description}\n";
				}
			}
			return response;
		}

		public async Task UpdateRefreshToken(string refreshToken,ApplicationUser user,
			DateTime accessTokenDate,int addOnAccessTokenDate)
		{
            if (user != null)
            {
                user.RefreshToken = refreshToken;
				user.RefreshTokenEndTime = accessTokenDate.AddSeconds(addOnAccessTokenDate);
				await _userManager.UpdateAsync(user);
            }
			else
			{
				throw new NotFoundUserException();
			}
			
        }
	}
}
