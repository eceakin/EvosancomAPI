using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvosancomAPI.Application.Features.ProductCategories.Commands.UpdateProductCategory
{
	public class UpdateProductCategoryCommandRequest :IRequest<UpdateProductCategoryCommandResponse>
	{
        public string Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
