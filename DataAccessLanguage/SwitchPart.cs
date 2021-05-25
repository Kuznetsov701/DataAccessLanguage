using DataAccessLanguage.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DataAccessLanguage
{
    public sealed class SwitchPart : IAsyncExpressionPart
    {
        private List<KeyValuePair<IExpression, IExpression>> expressions = new List<KeyValuePair<IExpression, IExpression>>();

        public ExpressionType Type => ExpressionType.Function;

        public SwitchPart(IExpressionFactory expressionFactory, string parameter)
        {
            Regex regex = new Regex(@"(?<expr>(((?!=>.*)[^(),=>]+)|(\((?>[^()]+|\((?<depth>)|\)(?<-depth>))*(?(depth)(?!))\)))+)([\s]*=>[\s]*(?<res>(((?!=>.*)[^(),=>]+)|(\((?>[^()]+|\((?<depth>)|\)(?<-depth>))*(?(depth)(?!))\)))+))*,{0,2}");

            var col = regex.Matches(parameter.Replace("\n.", ".").Replace("\n", " ").Replace("\r", " ").Replace("\t", " "));

            foreach (Match x in col)
            {
                if (x.Groups["expr"].Success && !string.IsNullOrWhiteSpace(x.Groups["expr"].Value) && x.Groups["res"].Success && !string.IsNullOrWhiteSpace(x.Groups["res"].Value))
                {
                    expressions.Add(KeyValuePair.Create(expressionFactory.Create(x.Groups["res"].Value), expressionFactory.Create(x.Groups["expr"].Value)));
                }
            }
        }

        public object GetValue(object obj) =>
            obj switch
            {
                IEnumerable<object> list => list.Select(x => Switch(x)).ToList(),
                object o => Switch(o),
                _ => Switch(null)
            };

        private object Switch(object o)
        {
            foreach (var expr in expressions)
            {
                if (bool.TryParse(expr.Value.GetValue(o)?.ToString(), out bool b) && b)
                    return expr.Key.GetValue(o);
            }
            return null;
        }

        public bool SetValue(object obj, object value) =>
            throw new NotImplementedException();

        public Task<object> GetValueAsync(object dataObject) =>
            Task.FromResult(GetValue(dataObject));

        public Task<bool> SetValueAsync(object dataObject, object value) =>
            Task.FromResult(SetValue(dataObject, value));
    }
}