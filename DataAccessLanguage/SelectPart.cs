using DataAccessLanguage.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessLanguage
{
    public sealed class SelectPart : IExpressionPart
    {
        private IExpression expression;

        public ExpressionType Type => ExpressionType.Function;

        public SelectPart(string parameter)
        {
            expression = new ExpressionFactory().Create(parameter);
        }

        public object GetValue(object obj) =>
            obj switch
            {
                IList<object> list => list.Select(x => expression.GetValue(x)).ToList(),
                _ => null
            };

        public bool SetValue(object obj, object value) =>
            obj switch
            {
                IList<object> list => list.Select(x => expression.SetValue(x, value)).ToList().Any(x => true),
                _ => false
            };
    }
}
