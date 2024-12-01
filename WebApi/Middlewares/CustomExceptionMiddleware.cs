using Newtonsoft.Json;
using System.Diagnostics;
using System.Net;

namespace WebApi.Middlewares
{
    // CustomExceptionMiddleware sınıfı, uygulama içerisinde özel hata yönetimi (exception handling)
    // yapılabilmesi için oluşturulmuş bir middleware'dir.
    public class CustomExceptionMiddleware
    {
        // RequestDelegate, bir HTTP isteği işlendiğinde yapılacak işlemleri belirten delegedir. 
        // _next, pipeline'daki bir sonraki middleware'i temsil eder.
        private readonly RequestDelegate _next;

        // Yapıcı metod, bir sonraki middleware'i alır.
        // Bu metod, middleware'in çalışmaya başlamadan önce gerekli olan bağımlılıkları alır.
        public CustomExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        // Invoke metodu, gelen HTTP isteği üzerinde işlemler yapar.
        // Bu metot, her HTTP isteği geldiğinde çağrılır ve burada hata yönetimi, loglama gibi işlemler yapılabilir.
        public async Task Invoke(HttpContext context)
        {
            var watch = Stopwatch.StartNew();
            try
            {
                string message = $"[Request] HTTP {context.Request.Method} - {context.Request.Path}";
                Debug.WriteLine(message);
                await _next(context);
                watch.Stop();
                message = $"[Response] HTTP {context.Request.Method} - {context.Request.Path}" +
                    $" responded {context.Response.StatusCode} in {watch.Elapsed.TotalMilliseconds} ms";
                Debug.WriteLine(message);
            }
            catch (Exception ex)
            {
                watch.Stop();
                await HandleException(context, ex, watch);
            }
        }

        private async Task HandleException(HttpContext context, Exception ex, Stopwatch watch)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            string message = $"Error HTTP {context.Request.Method} - {context.Response.StatusCode} Error Message" +
                $"{ex.Message} in {watch.Elapsed.Milliseconds} ms";
            Debug.WriteLine(message);

            var result = JsonConvert.SerializeObject(new { error = ex.Message }, Formatting.None);
            await context.Response.WriteAsync(result);
        }
    }

    // Bu statik sınıf, CustomExceptionMiddleware'in kolayca kullanılması için bir extension metod içerir.
    // Extension metodları, IApplicationBuilder'ı kullanarak middleware'i pipeline'a eklemeyi sağlar.
    public static class CustomExceptionMiddlewareExtension
    {
        // UseCustomExceptionMiddle, CustomExceptionMiddleware'i pipeline'a eklemek için kullanılan extension metodudur.
        // Bu metot, IApplicationBuilder nesnesine çağrıldığında middleware'i projeye dahil eder.
        public static IApplicationBuilder UseCustomExceptionMiddle(this IApplicationBuilder builder)
        {
            // Middleware'i pipeline'a ekler.
            return builder.UseMiddleware<CustomExceptionMiddleware>();
        }
    }
}
