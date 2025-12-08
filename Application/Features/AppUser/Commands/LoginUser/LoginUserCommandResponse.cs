
using EvosancomAPI.Application.DTOs;

namespace EvosancomAPI.Application.Features.AppUser.Commands.LoginUser
{
	public class LoginUserCommandResponse
	{
        public Token Token { get; set; }
        public string Message { get; set; }
    }
}