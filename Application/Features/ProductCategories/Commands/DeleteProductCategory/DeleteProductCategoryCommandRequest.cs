using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvosancomAPI.Application.Features.ProductCategories.Commands.DeleteProductCategory
{
	public class DeleteProductCategoryCommandRequest :IRequest<DeleteProductCategoryCommandResponse>
	{
		public string Id { get; set; }
	}
}
