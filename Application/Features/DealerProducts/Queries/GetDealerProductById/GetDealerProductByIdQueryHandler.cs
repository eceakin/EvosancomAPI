using MediatR;

namespace EvosancomAPI.Application.Features.DealerProducts.Queries.GetDealerProductById
{
	public
		class GetDealerProductByIdQueryHandler : IRequestHandler<GetDealerProductByIdQueryRequest, GetDealerProductByIdQueryResponse>
	{
		public Task<GetDealerProductByIdQueryResponse> Handle(GetDealerProductByIdQueryRequest request, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}
	}
}
