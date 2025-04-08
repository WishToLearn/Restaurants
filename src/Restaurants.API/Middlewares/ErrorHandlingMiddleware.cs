using Restaurants.Domain.Exceptions;

namespace Restaurants.API.Middlewares
{
    public class ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger) : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
			try
			{
				await next.Invoke(context);
			}
			catch(NotFoundException notFoundEx)
			{
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(notFoundEx.Message);

                logger.LogWarning(notFoundEx.Message);
            }
            catch(ForbidException)
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync("Access Forbidden");
            }
            catch (Exception exception)
			{
				context.Response.StatusCode = 500;
				await context.Response.WriteAsync("Something went wrong");

				logger.LogError(exception, exception.Message);
			}
        }
    }
}
