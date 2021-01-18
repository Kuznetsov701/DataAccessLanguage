using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessLanguage
{
    public sealed class TrimPart : IExpressionPart
    {
        private string separator;

        public ExpressionType Type => ExpressionType.Function;

        public TrimPart(string separator) => this.separator = separator;

        public object GetValue(object dataObject) =>
            dataObject switch
            {
                IEnumerable<string> list => list?.Select(x => x?.Trim(separator?.ToArray()))?.ToList(),
                string s => s?.Trim(separator?.ToArray())?.ToList(),
                IEnumerable<object> list => list?.Select(x => x?.ToString()?.Trim(separator?.ToArray()))?.ToList(),
                not null => dataObject?.ToString()?.Trim(separator?.ToArray())?.ToList(),
                _ => null
            };

        public bool SetValue(object dataObject, object value) =>
            throw new NotImplementedException();
    }
}