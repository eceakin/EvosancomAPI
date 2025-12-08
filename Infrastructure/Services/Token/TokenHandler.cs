using EvosancomAPI.Application.Abstractions.Token;
using EvosancomAPI.Domain.Entities.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EvosancomAPI.Infrastructure.Services.Token
{
	public class TokenHandler : ITokenHandler
	{
		readonly IConfiguration _configuration;
		public TokenHandler(IConfiguration configuration)
		{
			_configuration = configuration;
		}
		public Application.DTOs.Token CreateAccessToken(int second , ApplicationUser user)
		{
			Application.DTOs.Token token = new();

			//security key'in simetriğini oluştur
			SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));

			SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);

			token.Expiration = DateTime.UtcNow.AddSeconds(second);
			JwtSecurityToken jwtSecurityToken = new(
				issuer: _configuration["Token:Issuer"],
				audience: _configuration["Token:Audience"],
				notBefore: DateTime.UtcNow,
				expires: token.Expiration,
				signingCredentials: signingCredentials
				,
				claims: new List<Claim> { new(ClaimTypes.Name , user.UserName)}
				);

			// token oluşturucu sınıf
			JwtSecurityTokenHandler tokenHandler = new();
			token.AccessToken = tokenHandler.WriteToken(jwtSecurityToken);

			token.RefreshToken = CreateRefreshToken();
			return token;

		}

		public string CreateRefreshToken()
		{
			byte[] bytes = new byte[32];
			using RandomNumberGenerator randomNumberGenerator =
						RandomNumberGenerator.Create();
			randomNumberGenerator.GetBytes(bytes);
			return Convert.ToBase64String(bytes);


		}
	}
}
