
using EvosancomAPI.Application.DTOs.Dealer;
using EvosancomAPI.Application.Features.Products.DTOs;

namespace EvosancomAPI.Application.Abstractions.Services
{
	public interface IDealerService
	{
		Task CreateDealerAsync(CreateDealerDto dealer);
		Task UpdateDealerAsync(UpdateDealerDto dealer);
		Task DeleteDealerAsync(string id);
		// Listeleme işlemleri (Query tarafı için)
		Task<List<DealerListDto>> GetAllDealersAsync();
		Task<List<DealerProductListDto>> GetProductsForDealerAsync(string userId);
	}
}