using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace DataAccessLanguage.Demo.Blazor
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddSingleton<HttpClient>();
            builder.Services.AddSingleton<IJsLazyLoad, JsLazyLoad>();
            builder.Services.AddSingleton<ICssLazyLoad, CssLazyLoad>();
            builder.Services.AddSingleton<IExpressionFactory, ExpressionFactory>();

            await builder.Build().RunAsync();
        }
    }
}
