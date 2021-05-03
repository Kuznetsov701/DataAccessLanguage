using System;
using System.Threading.Tasks;

namespace DataAccessLanguage.Types
{
    public class StringFunction : IAsyncExpressionPart
    {
        private string value;

        public StringFunction(string value)
        {
            this.value = value;
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