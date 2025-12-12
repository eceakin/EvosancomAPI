using EvosancomAPI.Application.Abstractions.Services;
using EvosancomAPI.Application.CustomAttributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Reflection;

namespace EvosancomAPI.API.Filters
{

	public class RolePermissionFilter : IAsyncActionFilter
	{
		readonly IUserService _userService;

		public RolePermissionFilter(IUserService userService)
		{
			_userService = userService;
		}

		public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			var name = context.HttpContext.User.Identity?.Name;
			if (!string.IsNullOrEmpty(name) && name != "deneme")
			{
				var descriptor = context.ActionDescriptor as ControllerActionDescriptor;
				if (descriptor == null)
				{
					await next();
					return;
				}

				var attribute = descriptor.MethodInfo.GetCustomAttribute(typeof(AuthorizeDefinitionAttribute)) as AuthorizeDefinitionAttribute;
				var httpAttribute = descriptor.MethodInfo.GetCustomAttribute(typeof(HttpMethodAttribute)) as HttpMethodAttribute;

				string code = null;
				if (attribute != null)
				{
					code = $"{(httpAttribute != null ? httpAttribute.HttpMethods.First() : HttpMethods.Get)}.{attribute.ActionType}.{attribute.Definition.Replace(" ", "")}";
				}

				// If code is null, skip permission check or handle as needed
				if (code != null)
				{
					var hasRole = await _userService.HasRolePermissionToEndpointAsync(name, code);

					if (!hasRole)
						context.Result = new UnauthorizedResult();
					else
						await next();
				}
				else
				{
					await next();
				}
			}
			else
				await next();
		}

	}
}
