using Microsoft.JSInterop;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccessLanguage.Demo.Blazor
{
    public class CssLazyLoad : ICssLazyLoad
    {
        private readonly IJSRuntime jsRuntime;

        Dictionary<string, Task> loadings = new Dictionary<string, Task>();

        public CssLazyLoad(IJSRuntime jsRuntime)
        {
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
            string s = $"var a = document.createElement('link'); a.href='{ url }'; a.type='text/css'; a.rel='stylesheet'; document.head.appendChild(a);";
            await jsRuntime.InvokeVoidAsync("eval", s);
        }
    }
}