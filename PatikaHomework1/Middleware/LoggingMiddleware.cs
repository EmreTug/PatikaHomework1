using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace PatikaHomework1.Middleware
{
    // İsteklerin log dosyasına kaydedilmesini sağlar
    public class LoggingMiddleware
    {
        // İşlem yapmak istediğimiz diğer middleware'i tutar
        private readonly RequestDelegate _next;

        public LoggingMiddleware(RequestDelegate next)
        {
            // İşlem yapmak istediğimiz middleware'i _next değişkenine atarız
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Log dosyasının adı
            var logFile = "request_logs.txt";
            // İstek bilgileri
            var request = context.Request;
            // Log mesajı
            var logMessage = $"{DateTime.Now}: {request.Method} {request.Path}";

            // Log dosyasına yazmak için dosyayı açar
            using (var writer = File.AppendText(logFile))
            {
                // Log dosyasına log mesajını yazar
                await writer.WriteLineAsync(logMessage);
            }

            // İşlem yapmak istediğimiz diğer middleware'i çağırır
            await _next(context);
        }
    }
}
