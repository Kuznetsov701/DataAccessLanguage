﻿using DataAccessLanguage.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace DataAccessLanguage
{
    public sealed class MapPart : IExpressionPart
    {
        private Dictionary<string, IExpression> expressions = new Dictionary<string, IExpression>();

        public ExpressionType Type => ExpressionType.Function;

        public MapPart(string parameter)
        {
            IExpressionFactory expressionFactory = new ExpressionFactory();

            Regex regex = new Regex(@"(?<expr>(((?!=>.*)[^()&=>]+)|(?<kek>\((?>[^()]+|\((?<depth>)|\)(?<-depth>))*(?(depth)(?!))\)))+)([\s]*=>[\s]*(?'name'[\w\d]+))*&{0,2}");

            var col = regex.Matches(parameter.Replace("\n.", ".").Replace("\n", " ").Replace("\r", " ").Replace("\t", " "));

            int nonameCount = 0;
            foreach (Match x in col)
            {
                if (x.Groups["expr"].Success && !string.IsNullOrWhiteSpace(x.Groups["expr"].Value))
                {
                    string name = x.Groups["name"].Value;
                    if (string.IsNullOrWhiteSpace(name))
                    {
                        name = "noname" + ((nonameCount == 0) ? "" : "_" + nonameCount.ToString());
                        nonameCount++;
                    }
                    expressions.Add(name, expressionFactory.Create(x.Groups["expr"].Value));
                }
            }
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
