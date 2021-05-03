using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccessLanguage
{
    public class DistinctPart : IAsyncExpressionPart
    {
        public ExpressionType Type => ExpressionType.Function;

        private IExpression expression;

        public DistinctPart(IExpressionFactory expressionFactory, string parameter)
        {
            expression = expressionFactory.Create(parameter);
        }

        public object GetValue(object dataObject) =>
            dataObject switch
            {
                IEnumerable<object> e => e.Select(x => expression.GetValue(x)).Distinct(),
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
