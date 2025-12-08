using EvosancomAPI.Application.DTOs;
using EvosancomAPI.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvosancomAPI.Application.Abstractions.Token
{
	public interface ITokenHandler
	{
		DTOs.Token CreateAccessToken(int second , ApplicationUser user);
		string CreateRefreshToken();


	}
}
