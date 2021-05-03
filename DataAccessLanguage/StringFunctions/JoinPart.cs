using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccessLanguage
{
    public sealed class JoinPart : IAsyncExpressionPart
    {
        private string separator;

        public ExpressionType Type => ExpressionType.Function;

        public JoinPart(string separator) => this.separator = separator;

        public object GetValue(object dataObject) =>
            dataObject switch
            {
                IEnumerable<object> list => string.Join(separator, list),
                not null => string.Join(separator, dataObject),
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