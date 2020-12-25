using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessLanguage
{
    public class GroupByPart : IExpressionPart
    {
        public ExpressionType Type => ExpressionType.Function;

        private IExpression expression;

        public GroupByPart(string parameter)
        {
            expression = new ExpressionFactory().Create(parameter);
        }

        public object GetValue(object dataObject) =>
            dataObject switch
            {
                IEnumerable<object> e => e.GroupBy(x => expression.GetValue(x)),
                _ => null
            };

        public bool SetValue(object dataObject, object value)
        {
            throw new NotImplementedException();
        }
    }
}
