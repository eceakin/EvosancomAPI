using EvosancomAPI.Application.Repositories.PriceCalculation;
using EvosancomAPI.Domain.Entities;
using EvosancomAPI.Persistence.Contexts;

namespace EvosancomAPI.Persistence.Repositories.PricaCalculation
{
	public class PriceCalculationHistoryWriteRepository : WriteRepository<PriceCalculationHistory> , IPriceCalculationHistoryWriteRepository
	{
		public PriceCalculationHistoryWriteRepository(ApplicationDbContext context) : base(context)
		{
		}
	}
}
