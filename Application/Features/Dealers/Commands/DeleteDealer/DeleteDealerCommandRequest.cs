using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvosancomAPI.Application.Features.Dealers.Commands.DeleteDealer
{
	public class DeleteDealerCommandRequest:IRequest<DeleteDealerCommandResponse>
	{
		public string Id { get; set; }

	}
}
