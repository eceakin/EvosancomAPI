using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using System.Net.Mime;
using System.Text.Json;

namespace EvosancomAPI.API.Extensions
{
	public static class ConfigureExceptionHandlerExtension
	{
		// extension old için static

		public static void ConfigureExceptionHandler<T>(this WebApplication application,ILogger<T> logger)
		{
			application.UseExceptionHandler(
				builder =>
				{
					builder.Run(async context =>
					{//delegate,event,lambda ileri düzey
						context.Response.StatusCode =
						(int)HttpStatusCode.InternalServerError;

						context.Response.ContentType = MediaTypeNames.Application.Json;

						var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
						if (contextFeature != null)
						{
							logger.LogError($"Something went wrong in the {contextFeature.Error.Message}");
							await context.Response.WriteAsync(JsonSerializer.Serialize(
								new
								{
									StatusCode = context.Response.StatusCode,
									Message = contextFeature.Error.Message,
									Title = "Hata Oluştu"
								}
								)); ;
						}
					});
				}
				); // middleware


		}
	}
}
