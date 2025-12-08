using EvosancomAPI.Application.DTOs.User;
using EvosancomAPI.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvosancomAPI.Application.Abstractions.Services
{
	public interface IUserService
	{
		Task<CreateUserResponseDto> CreateAsync(CreateUserDto model);
		Task UpdateRefreshToken(string refreshToken,
			ApplicationUser applicationUser,DateTime accessTokenDate,int addOnAccessTokenDate);

	
	}
}
