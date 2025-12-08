using EvosancomAPI.Application.Abstractions.Services;
using EvosancomAPI.Application.Abstractions.Token;
using EvosancomAPI.Application.DTOs;
using EvosancomAPI.Application.Exceptions;
using EvosancomAPI.Domain.Entities.Identity;
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

	public class AuthService : IAuthService
	{
		readonly UserManager<ApplicationUser> _userManager;
		readonly SignInManager<ApplicationUser> _signInManager;
		readonly ITokenHandler _tokenHandler;
		readonly IUserService _userService;

		public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ITokenHandler tokenHandler, IUserService userService)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_tokenHandler = tokenHandler;
			_userService = userService;
		}

		public async Task<Token> LoginAsync(string usernameOrEmail, string password, int accessTokenLifeTime)
		{
			ApplicationUser user = await _userManager.FindByNameAsync(usernameOrEmail);
			if (user == null)
			{
				user = await _userManager.FindByEmailAsync(usernameOrEmail);
			}
			if (user == null)
			{
				throw new NotFoundUserException();
			}

			SignInResult signInResult = await _signInManager.CheckPasswordSignInAsync(user, password, false);

			if (signInResult.Succeeded)//auth başarılı 
			{
				Token token = _tokenHandler.CreateAccessToken(accessTokenLifeTime ,user);
				await _userService.UpdateRefreshToken(token.RefreshToken, user,
					token.Expiration, 900);
				return token;
			}
			throw new Exception("Kullanıcı adı veya parola hatalı");
		}

		public async Task<Token> RefreshTokenLoginAsync(string refreshToken)
		{
			ApplicationUser? user = await _userManager.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
			if (user != null && user?.RefreshTokenEndTime > DateTime.UtcNow)
			{
				Token token = _tokenHandler.CreateAccessToken(900,user);
				await _userService.UpdateRefreshToken(token.RefreshToken, user,
					token.Expiration, 900);
				return token;
			}
			throw new NotFoundUserException();
		}
	}
}
