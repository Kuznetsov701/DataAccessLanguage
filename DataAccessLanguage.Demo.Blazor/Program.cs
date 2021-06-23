using DataAccessLanguage.Extensions;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Text.Json;
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

            var jsonOptions = new JsonSerializerOptions();
            jsonOptions.Converters.Add(new JsonToDictionaryConverter());
            builder.Services.AddDataAccessLanguage(null, jsonOptions);

            await builder.Build().RunAsync();
        }
    }
}
