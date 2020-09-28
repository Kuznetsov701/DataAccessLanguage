using System;
using System.Collections.Generic;
using System.Linq;



namespace DataAccessLanguage
{
    public class WherePart : IExpressionPart
    {
        private IExpression expression;

        public ExpressionType Type => ExpressionType.Function;

        public WherePart(string parameter)
        {
            expression = new ExpressionFactory().Create(parameter);
        }

        public object GetValue(object obj) =>
            obj switch
            {
                IList<object> list => list.Select(x => new { obj = x, result = expression.GetValue(x) }).Where( r => { Boolean.TryParse(r.result?.ToString(), out bool res); return res; }).Select(x => x.obj).ToList(),
                _ => null
            };



        public bool SetValue(object dataObject, object value) =>
            throw new NotImplementedException();
    }
}
