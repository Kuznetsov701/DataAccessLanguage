using DataAccessLanguage.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessLanguage
{
    public sealed class MapPart : IExpressionPart
    {
        private Dictionary<string, IExpression> expressions = new Dictionary<string, IExpression>();

        public ExpressionType Type => ExpressionType.Function;

        public MapPart(string parameter)
        {
            IExpressionFactory expressionFactory = new ExpressionFactory();

            parameter.Split("&&").Foreach(x =>
            {
                string[] e = x.Split(" as ");
                if (e.Length > 1)
                    expressions.Add(e[1].Trim(' '), expressionFactory.Create(e[0]));
                else
                    expressions.Add(e[0].Split('.')[0].Trim(' '), expressionFactory.Create(e[0]));
            });
        }

        public object GetValue(object obj) =>
            obj switch
            {
                IEnumerable<object> list => list.Select(x => Map(x)).ToList(),
                object o => Map(o),
                _ => null
            };

        private object Map(object o)
        {
            Dictionary<string, object> res = new Dictionary<string, object>();
            foreach (var expr in expressions)
                res.TryAdd(expr.Key, expr.Value.GetValue(o));
            return res;
        }

        public bool SetValue(object obj, object value) =>
            throw new NotImplementedException();
    }
}
