using EvosancomAPI.Application.Abstractions.Services;
using MediatR;

namespace EvosancomAPI.Application.Features.AppRole.Queries.GetRoleById
{
	public class GetRoleByIdQueryHandler : IRequestHandler<GetRoleByIdQueryRequest, GetRoleByIdQueryResponse>

	{
		readonly IRoleService _roleService;
		public async Task<GetRoleByIdQueryResponse> Handle(GetRoleByIdQueryRequest request, CancellationToken cancellationToken)
		{
			var data = await _roleService.GetRoleById(request.Id);
			return new()
			{
				Id = data.id,
				Name = data.name

			};
			}
	}
}
