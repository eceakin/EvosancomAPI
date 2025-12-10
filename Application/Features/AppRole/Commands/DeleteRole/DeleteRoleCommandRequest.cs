using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvosancomAPI.Application.Features.AppRole.Commands.DeleteRole
{
	public class DeleteRoleCommandRequest:IRequest<DeleteRoleCommandResponse>
	{
        public string Id { get; set; }
    }
}
