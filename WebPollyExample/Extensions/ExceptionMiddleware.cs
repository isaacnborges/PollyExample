using Microsoft.AspNetCore.Http;
using Polly.CircuitBreaker;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebPollyExample.Extensions
{
    public class PollyExampleExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public PollyExampleExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (HttpRequestException)
            {
                HandleSocketException(httpContext);
            }
            catch (BrokenCircuitException)
            {
                HandleCircuitBreakerException(httpContext);
            }
        }

        private static void HandleSocketException(HttpContext context) =>
            context.Response.Redirect("error/request-exception");

        private static void HandleCircuitBreakerException(HttpContext context) =>
            context.Response.Redirect("error/circuit-breaker");
    }
}