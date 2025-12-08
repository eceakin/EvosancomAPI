using EvosancomAPI.Application.Commons.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvosancomAPI.Application.Features.Products.Commands.DeleteProduct
{
	public class DeleteProductCommandRequest : IRequest<DeleteProductCommandResponse>
	{
        public string Id  { get; set; }	
    }

}
