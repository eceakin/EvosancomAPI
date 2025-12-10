using EvosancomAPI.Application.Abstractions.Services;
using MediatR;

namespace EvosancomAPI.Application.Features.AppRole.Queries.GetAllRoles
{
	public class GetAllRolesQueryHandler : IRequestHandler<GetAllRolesQueryRequest, GetAllRolesQueryResponse>
	{
		readonly IRoleService _roleService;

		public GetAllRolesQueryHandler(IRoleService roleService)
		{
			_roleService = roleService;
		}

		public async Task<GetAllRolesQueryResponse> Handle(GetAllRolesQueryRequest request, CancellationToken cancellationToken)
		{
			var datas = _roleService.GetAllRoles();
			return new()
			{
				Datas = datas,
				//TotalRoleCount = datas.Count
			};
		}
	}
}
