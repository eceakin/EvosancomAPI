using EvosancomAPI.Application.Repositories.PriceCalculation;
using EvosancomAPI.Domain.Entities;
using EvosancomAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvosancomAPI.Persistence.Repositories.PricaCalculation
{
	public class PriceCalculationHistoryReadRepository :ReadRepository<PriceCalculationHistory> , IPriceCalculationHistoryReadRepository
	{
		public PriceCalculationHistoryReadRepository(ApplicationDbContext context) : base(context)
		{
		}
	
	}
}
