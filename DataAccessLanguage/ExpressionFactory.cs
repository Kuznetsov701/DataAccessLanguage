using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DataAccessLanguage
{
    public class ExpressionFactory : IExpressionFactory, IAsyncExpressionFactory
    {
        Regex regex = new Regex(@"(?<index>\[(?<ivalue>\d+)\])|(?<function>(?<fname>[\w\d]+)\((?<fparams>(?>[^()]+|\((?<depth>)|\)(?<-depth>))*(?(depth)(?!)))\))|(?<selector>[\w\d]+)");

        public ExpressionFactory(Func<ExpressionFactory, Dictionary<string, Func<string, IAsyncExpressionPart>>> expressionProvidersBuilder)
        {
            this.expressionProviders = expressionProvidersBuilder.Invoke(this);
        }

        IExpression IExpressionFactory.Create(string expression)
        {
            IExpression exp = new Expression();
            foreach (Match i in regex.Matches(expression))
            {
                if (i.Groups["selector"].Success)
                    exp.Add(expressionProviders["selector"].Invoke(i.Groups["selector"].Value));
                else if (i.Groups["index"].Success)
                    exp.Add(expressionProviders["index"].Invoke(i.Groups["ivalue"].Value));
                else if (i.Groups["function"].Success)
                    exp.Add(expressionProviders[i.Groups["fname"].Value].Invoke(i.Groups["fparams"].Value));
            }
            return exp;
        }

        IAsyncExpression IAsyncExpressionFactory.Create(string expression)
        {
            IAsyncExpression exp = new Expression();
            foreach (Match i in regex.Matches(expression))
            {
                if (i.Groups["selector"].Success)
                    exp.Add(expressionProviders["selector"].Invoke(i.Groups["selector"].Value));
                else if (i.Groups["index"].Success)
                    exp.Add(expressionProviders["index"].Invoke(i.Groups["ivalue"].Value));
                else if (i.Groups["function"].Success)
                    exp.Add(expressionProviders[i.Groups["fname"].Value].Invoke(i.Groups["fparams"].Value));
            }
            return exp;
        }

        private Dictionary<string, Func<string, IAsyncExpressionPart>> expressionProviders;
    }
}