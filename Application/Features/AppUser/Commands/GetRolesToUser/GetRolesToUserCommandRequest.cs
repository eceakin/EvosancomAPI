using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvosancomAPI.Application.Features.AppUser.Commands.GetRolesToUser
{
	public class GetRolesToUserCommandRequest:IRequest<GetRolesToUserCommandResponse>
	{
        public string UserId { get; set; }
    }
	
}
