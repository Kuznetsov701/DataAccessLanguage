using System;
using System.Threading.Tasks;

namespace DataAccessLanguage.Types
{
    public class LongFunction : IAsyncExpressionPart
    {
        private long? value;

        public LongFunction(string value)
        {
            if (long.TryParse(value, out long v))
                this.value = v;
        }

        public ExpressionType Type => ExpressionType.Function;

        public object GetValue(object dataObject) => value;

        public bool SetValue(object dataObject, object value) =>
            throw new NotImplementedException();

        public Task<object> GetValueAsync(object dataObject) =>
            Task.FromResult(GetValue(dataObject));

        public Task<bool> SetValueAsync(object dataObject, object value) =>
            Task.FromResult(SetValue(dataObject, value));
    }
}