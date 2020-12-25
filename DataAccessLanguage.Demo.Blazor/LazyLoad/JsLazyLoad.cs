using Microsoft.JSInterop;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace DataAccessLanguage.Demo.Blazor
{
    public class JsLazyLoad : IJsLazyLoad
    {
        private readonly IJSRuntime jsRuntime;
        private readonly HttpClient httpClient;

        Dictionary<string, Task> loadings = new Dictionary<string, Task>();

        public JsLazyLoad(IJSRuntime jsRuntime, HttpClient httpClient)
        {
            this.httpClient = httpClient;
            this.jsRuntime = jsRuntime;
        }

        public Task LoadAsync(string url)
        {
            if (!loadings.ContainsKey(url))
                loadings.Add(url, Load(url));

            return loadings[url];
        }

        internal async Task Load(string url)
        {
            string code = await (await httpClient.GetAsync(url)).Content.ReadAsStringAsync();
            await jsRuntime.InvokeVoidAsync("eval", code);
        }
    }
}