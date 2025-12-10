using EvosancomAPI.Application;
using EvosancomAPI.Application.Abstractions.Services;
using EvosancomAPI.Application.DTOs.User;
using EvosancomAPI.Application.Exceptions;
using EvosancomAPI.Application.Features.AppUser.Commands.CreateUser;
using EvosancomAPI.Domain.Entities.Identity;
using EvosancomAPI.Domain.Entities.Role;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
		readonly IEndpointReadRepository _endpointReadRepository;

		public UserService(UserManager<ApplicationUser> userManager, IEndpointReadRepository endpointReadRepository)
		{
			_userManager = userManager;
			_endpointReadRepository = endpointReadRepository;
		}

		public int TotalUsersCount => _userManager.Users.Count();



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

		public async Task UpdateRefreshToken(string refreshToken, ApplicationUser user,
			DateTime accessTokenDate, int addOnAccessTokenDate)
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
		public async Task AssingRoleToUserAsync(string userId, string[] roles)
		{
			ApplicationUser applicationUser = await _userManager.FindByIdAsync(userId);
			if (applicationUser != null)
			{
				var userRoles = await _userManager.GetRolesAsync(applicationUser);
				await _userManager.RemoveFromRolesAsync(applicationUser, userRoles);
				await _userManager.AddToRolesAsync(applicationUser, roles);
			}

		}

		public async Task<string[]> GetRolesToUserAsync(string userIdOrName)
		{
			ApplicationUser applicationUser = await _userManager.FindByIdAsync(userIdOrName);
			if (applicationUser == null)
			{
				applicationUser = await _userManager.FindByNameAsync(userIdOrName);
			}
			if (applicationUser != null)
			{
				var userRoles = await _userManager.GetRolesAsync(applicationUser);
				return userRoles.ToArray();

			}
			throw new Exception("bu role ait kullanıcı bulunamadı");
		}
		public async Task<List<ListUserDto>> GetAllUsersAsync()
		{
			var users = await _userManager.Users.ToListAsync();
			return users.Select(user => new ListUserDto
			{
				Id = user.Id,
				Email = user.Email,
				FirstName = user.FirstName,
				LastName = user.LastName,
				TwoFactorEnabled = user.TwoFactorEnabled,
				UserName = user.UserName

			}).ToList();
		}


		public async Task<bool> HasRolePermissionToEndpointAsync(string userName, string code)
		{
			var userRoles = await GetRolesToUserAsync(userName);
			if (!userRoles.Any()) {
				return false;
			}
			Endpoint endpoint = await _endpointReadRepository.Table
				.Include(e => e.Roles)
				.FirstOrDefaultAsync(e => e.Code == code);
			if (endpoint == null)
			{
				return false;
			}
			var hasRole = false;
			var endpointRoles = endpoint.Roles.Select(r => r.Name);
			foreach (var userRole in userRoles)
			{
				if (!hasRole)
				{
					foreach (var endpointRole in endpointRoles)
					{
						if (userRole == endpointRole)
						{
							hasRole = true;
							break;
						}
					}
				}
				else
				{
					break;
				}
				
			}
			return hasRole;
		}
	}
}
