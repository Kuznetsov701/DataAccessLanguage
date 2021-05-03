using System;
using System.Threading.Tasks;

namespace DataAccessLanguage.Types
{
    public class ByteFunction : IAsyncExpressionPart
    {
        private byte? value;

        public ByteFunction(string value)
        {
            if (byte.TryParse(value, out byte v))
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