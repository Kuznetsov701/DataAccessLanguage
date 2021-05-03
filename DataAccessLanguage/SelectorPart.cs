using DataAccessLanguage.Extensions;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace DataAccessLanguage
{
    public sealed class SelectorPart : IAsyncExpressionPart
    {
        private string key;

        public ExpressionType Type => ExpressionType.Selector;

        public SelectorPart(string key) => this.key = key;

        public object GetValue(object dataObject) =>
            dataObject switch {
                IDictionary<string, object> dictionary when dictionary.ContainsKey(key) => dictionary[key],
                object obj when obj.TryGetPropertyInfo(key, out PropertyInfo property) => property.GetValue(obj),
                _ => null
            };

        public bool SetValue(object dataObject, object value) =>
            dataObject switch {
                IDictionary<string, object> dictionary => dictionary.TrySetValue(key, value),
                object obj when obj.TryGetPropertyInfo(key, out PropertyInfo property) => property.TrySetValue(obj, value),
                _ => false
            };

        public Task<object> GetValueAsync(object dataObject) =>
            Task.FromResult(GetValue(dataObject));

        public Task<bool> SetValueAsync(object dataObject, object value) =>
            Task.FromResult(SetValue(dataObject, value));
    }
}