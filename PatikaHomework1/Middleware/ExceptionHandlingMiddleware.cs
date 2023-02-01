using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace PatikaHomework1.Middleware
{
    // HttpContext içindeki herhangi bir hata durumunda, bu middleware çalışacak
    public class ExceptionHandlingMiddleware
    {
        // İşlem yapmak istediğimiz diğer middleware'i tutar
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            // İşlem yapmak istediğimiz middleware'i _next değişkenine atarız
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // İşlem yapmak istediğimiz middleware'i çağırır
                await _next(context);
            }
            catch (Exception ex)
            {
                // Herhangi bir hata olduğunda, HandleExceptionAsync metodunu çağırır
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            // İstek yanıtının veri tipini metin olarak belirler
            context.Response.ContentType = "text/plain";
            // İstek yanıtının durum kodunu 500 (Internal Server Error) olarak ayarlar
            context.Response.StatusCode = 500;

            // İstek yanıtında hata mesajını yazar
            return context.Response.WriteAsync(exception.ToString(), Encoding.UTF8);
        }
    }
}
