using System.Threading.Tasks;

namespace DataAccessLanguage.Demo.Blazor
{
    public interface ILazyLoad
    {
        Task LoadAsync(string url);
    }
}