using DataAccessLanguage.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccessLanguage
{
    public sealed class SelectPart : IAsyncExpressionPart
    {
        private IExpression expression;

        public ExpressionType Type => ExpressionType.Function;

        public SelectPart(IExpressionFactory expressionFactory, string parameter)
        {
            expression = expressionFactory.Create(parameter);
        }

        public object GetValue(object obj) =>
            obj switch
            {
                IEnumerable<object> list => list.Select(x => expression.GetValue(x)).ToList(),
                _ => null
            };

        public bool SetValue(object obj, object value) =>
            obj switch
            {
                IEnumerable<object> list => list.Select(x => expression.SetValue(x, value)).ToList().Any(x => true),
                _ => false
            };

        public Task<object> GetValueAsync(object dataObject) =>
            Task.FromResult(GetValue(dataObject));

        public Task<bool> SetValueAsync(object dataObject, object value) =>
            Task.FromResult(SetValue(dataObject, value));
    }
}