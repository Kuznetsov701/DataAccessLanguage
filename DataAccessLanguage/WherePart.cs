using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccessLanguage
{
    public class WherePart : IAsyncExpressionPart
    {
        private IExpression expression;

        public ExpressionType Type => ExpressionType.Function;

        public WherePart(IExpressionFactory expressionFactory, string parameter)
        {
            expression = expressionFactory.Create(parameter);
        }

        public object GetValue(object obj) =>
            obj switch
            {
                IEnumerable<object> list => list.Select(x => new { obj = x, result = expression.GetValue(x) }).Where( r => { bool.TryParse(r.result?.ToString(), out bool res); return res; }).Select(x => x.obj).ToList(),
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