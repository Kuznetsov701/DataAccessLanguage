using System.Threading.Tasks;

namespace DataAccessLanguage
{
    public interface IAsyncExpressionPart : IExpressionPart
    {
        Task<object> GetValueAsync(object dataObject);
        Task<bool> SetValueAsync(object dataObject, object value);
    }
}