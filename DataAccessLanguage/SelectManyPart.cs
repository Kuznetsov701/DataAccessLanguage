using System.Collections.Generic;
using System.Linq;

namespace DataAccessLanguage
{
    class SelectManyPart : IExpressionPart
    {
        private IExpression expression;

        public ExpressionType Type => ExpressionType.Function;

        public SelectManyPart(string parameter)
        {
            expression = new ExpressionFactory().Create(parameter);
        }

        public object GetValue(object obj) =>
            obj switch
            {
                IEnumerable<object> list => list.SelectMany(x => expression.GetValue(x) as IEnumerable<object> ?? Enumerable.Empty<object>()).ToList(),
                _ => null
            };

        public bool SetValue(object obj, object value) =>
            obj switch
            {
                IEnumerable<object> list => list.Select(x => expression.SetValue(x, value)).ToList().Any(x => true),
                _ => false
            };
    }
}
