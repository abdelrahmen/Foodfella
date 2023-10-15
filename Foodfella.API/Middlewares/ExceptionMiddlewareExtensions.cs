using Foodfella.Core.Models;
using System.Text.Json;

namespace Foodfella.API.Extentions
{
	public class GlobalExceptionHandlingMiddleware : IMiddleware
	{
		public async Task InvokeAsync(HttpContext context, RequestDelegate next)
		{
			try
			{
				await next(context);
			}
			catch (Exception ex)
			{
				context.Response.StatusCode = 500;
				var errorDetails = new ErrorDetails
				{
					StatusCode = 500,
					Message = ex.Message,
				};

				var json = JsonSerializer.Serialize(errorDetails);
				
				await context.Response.WriteAsync(json);

				context.Response.ContentType = "application/json";
			}
		}
	}
}
