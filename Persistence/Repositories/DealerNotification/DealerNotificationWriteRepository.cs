using EvosancomAPI.Application.Repositories.DealerNotification;
using EvosancomAPI.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvosancomAPI.Persistence.Repositories.DealerNotification
{
	public class DealerNotificationWriteRepository :
		WriteRepository<Domain.Entities.DealerNotification>, IDealerNotificationWriteRepository
	{
		public DealerNotificationWriteRepository(ApplicationDbContext context) : base(context)
		{
		}
	}
}
