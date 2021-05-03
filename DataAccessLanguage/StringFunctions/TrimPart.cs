using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccessLanguage
{
    public sealed class TrimPart : IAsyncExpressionPart
    {
        private string separator;

        public ExpressionType Type => ExpressionType.Function;

        public TrimPart(string separator) => this.separator = separator;

        public object GetValue(object dataObject) =>
            dataObject switch
            {
                IEnumerable<string> list => list?.Select(x => x?.Trim(separator?.ToArray()))?.ToList(),
                string s => s?.Trim(separator?.ToArray()),
                IEnumerable<object> list => list?.Select(x => x?.ToString()?.Trim(separator?.ToArray()))?.ToList(),
                not null => dataObject?.ToString()?.Trim(separator?.ToArray()),
                _ => null
            };

        public bool SetValue(object dataObject, object value) =>
            throw new NotImplementedException();

        public Task<object> GetValueAsync(object dataObject) =>
            Task.FromResult(GetValue(dataObject));

        public Task<bool> SetValueAsync(object dataObject, object value) =>
            Task.FromResult(SetValue(dataObject, value));
    }
}