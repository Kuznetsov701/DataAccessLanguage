using System;
using System.Threading.Tasks;

namespace DataAccessLanguage
{
    public class SelfPart : IAsyncExpressionPart
    {
        public ExpressionType Type => ExpressionType.Function;

        public object GetValue(object dataObject) => dataObject;

        public bool SetValue(object dataObject, object value) => throw new NotImplementedException("self() setValue not implemented");

        public Task<object> GetValueAsync(object dataObject) =>
            Task.FromResult(GetValue(dataObject));

        public Task<bool> SetValueAsync(object dataObject, object value) =>
            Task.FromResult(SetValue(dataObject, value));
    }
}