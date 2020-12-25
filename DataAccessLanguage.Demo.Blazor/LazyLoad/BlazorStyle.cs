using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace DataAccessLanguage.Demo.Blazor
{
    public class BlazorStyle : ComponentBase
    {
        [Inject]
        public ICssLazyLoad CssLazyLoad { get; set; }

        [Parameter]
        public string Href { get; set; }

        protected override Task OnInitializedAsync() => CssLazyLoad.LoadAsync(Href);
    }
}