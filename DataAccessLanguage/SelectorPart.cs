using DataAccessLanguage.Extensions;
using System.Collections.Generic;
using System.Reflection;

namespace DataAccessLanguage
{
    public sealed class SelectorPart : IExpressionPart
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
    }
}