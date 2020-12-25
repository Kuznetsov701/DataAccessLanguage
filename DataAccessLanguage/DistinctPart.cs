using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessLanguage
{
    public class DistinctPart : IExpressionPart
    {
        public ExpressionType Type => ExpressionType.Function;

        private IExpression expression;

        public DistinctPart(string parameter)
        {
            expression = new ExpressionFactory().Create(parameter);
        }

        public object GetValue(object dataObject) =>
            dataObject switch
            {
                IEnumerable<object> e => e.Select(x => expression.GetValue(x)).Distinct(),
                _ => null
            };

        public bool SetValue(object dataObject, object value)
        {
            throw new NotImplementedException();
        }
    }
}
