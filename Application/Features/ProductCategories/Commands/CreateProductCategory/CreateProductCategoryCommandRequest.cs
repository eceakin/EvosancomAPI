using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvosancomAPI.Application.Features.ProductCategories.Commands.CreateProductCategory
{
	public class CreateProductCategoryCommandRequest : IRequest<CreateProductCategoryCommandResponse>
	{
		public string Name { get; set; } = string.Empty;

    }
}
