using EvosancomAPI.Application.Abstractions.Services;
using MediatR;

namespace EvosancomAPI.Application.Features.AuthorizationEndpoints.Queries.GetRolesToEndpoints
{
	public class GetRolesToEndpointQueryHandler : IRequestHandler<GetRolesToEndpointQueryRequest, GetRolesToEndpointQueryResponse>
	{
		IAuthorizationEndpointService _authorizationEndpointService;

		public GetRolesToEndpointQueryHandler(IAuthorizationEndpointService authorizationEndpointService)
		{
			_authorizationEndpointService = authorizationEndpointService;
		}

		public async Task<GetRolesToEndpointQueryResponse> Handle(GetRolesToEndpointQueryRequest request, CancellationToken cancellationToken)
		{
			var datas = await _authorizationEndpointService.GetRolesToEndpointAsync(request.Code , request.Menu);

			return new() {
				Roles = datas
			};
		}
	}
}
