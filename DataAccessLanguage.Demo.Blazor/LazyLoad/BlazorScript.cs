using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace DataAccessLanguage.Demo.Blazor
{
    public class BlazorScript : ComponentBase
    {
        [Inject]
        public IJsLazyLoad CssLazyLoad { get; set; }

        [Parameter]
        public string Src { get; set; }

        protected override Task OnInitializedAsync() => CssLazyLoad.LoadAsync(Src);
    }
}