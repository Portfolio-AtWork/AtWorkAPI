using AtWork.Shared.Enums.Models;
using AtWork.Shared.Models;
using System.Net;
using System.Text.Json;

namespace AtWorkAPI.Middlewares
{
    public class AtWorkMiddleware(RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception err)
            {
                await HandleExceptionAsync(context, err);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            List<Notification> notifications = [new(exception.Message, NotificationKind.Error)];

            object errorResponse = new
            {
                value = null as string,
                notifications,
                ok = false
            };

            string errorJson = JsonSerializer.Serialize(errorResponse);
            return context.Response.WriteAsync(errorJson);
        }
    }
}
