using System;
using System.Threading.Tasks;

namespace DataAccessLanguage
{
    public class NotEqualsPart : IAsyncExpressionPart
    {
        private string parameter;
        public ExpressionType Type => ExpressionType.Function;

        public NotEqualsPart(string parameter)
        {
            this.parameter = parameter;
        }

        public object GetValue(object obj) =>
            obj switch
            {
                not null when parameter == "not null" => false,
                null when parameter == "null" => false,
                object o when o.ToString().Equals(parameter) => false,
                _ => true
            };

        public bool SetValue(object obj, object value) =>
            throw new NotImplementedException();

        public Task<object> GetValueAsync(object dataObject) =>
            Task.FromResult(GetValue(dataObject));

        public Task<bool> SetValueAsync(object dataObject, object value) =>
            Task.FromResult(SetValue(dataObject, value));
    }
}