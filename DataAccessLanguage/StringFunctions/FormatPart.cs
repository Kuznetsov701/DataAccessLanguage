using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccessLanguage
{
    public sealed class FormatPart : IAsyncExpressionPart
    {
        private string format;

        public ExpressionType Type => ExpressionType.Function;

        public FormatPart(string format) => this.format = format;

        public object GetValue(object dataObject) =>
            dataObject switch
            {
                IEnumerable<object> list => list?.Select(x => string.Format("{0:" + format + "}", x))?.ToList(),
                not null => string.Format("{0:" + format + "}", dataObject),
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