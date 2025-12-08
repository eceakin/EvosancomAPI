using EvosancomAPI.Application.Abstractions.Services.Authentications;

namespace EvosancomAPI.Application.Abstractions.Services
{
	public interface IAuthService :IExternalAuthentication , IInternalAuthentication
	{
	}
}
