using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccessLanguage
{
    public sealed class SplitPart : IAsyncExpressionPart
    {
        private string separator;

        public ExpressionType Type => ExpressionType.Function;

        public SplitPart(string separator) => this.separator = separator;

        public object GetValue(object dataObject) =>
            dataObject switch
            {
                IEnumerable<string> list => list?.Select(x => x?.Split(separator))?.ToList(),
                string s => s?.Split(separator)?.ToList(),
                IEnumerable<object> list => list?.Select(x => x?.ToString()?.Split(separator))?.ToList(),
                not null => dataObject?.ToString()?.Split(separator)?.ToList(),
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