using EvosancomAPI.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvosancomAPI.Persistence.Repositories.DealerNotification
{
	public class DealerNotificationReadRepository :ReadRepository<Domain.Entities.DealerNotification> , IDealerNotificationReadRepository
	{
		public DealerNotificationReadRepository(Contexts.ApplicationDbContext context) : base(context)
		{
		}
	{
	}
}
