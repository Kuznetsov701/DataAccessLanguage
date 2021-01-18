using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DataAccessLanguage
{
    public class ExpressionFactory : IExpressionFactory
    {
        Regex regex = new Regex(@"(?<index>\[(?<ivalue>\d+)\])|(?<function>(?<fname>[\w\d]+)\((?<fparams>(?>[^()]+|\((?<depth>)|\)(?<-depth>))*(?(depth)(?!)))\))|(?<selector>[\w\d]+)");

        public IExpression Create(string expression)
        {
            IExpression exp = new Expression();
            foreach (Match i in regex.Matches(expression))
            {
                if (i.Groups["selector"].Success)
                    exp.Add(RegisteredTypes["selector"].Invoke(i.Groups["selector"].Value));
                else if (i.Groups["index"].Success)
                    exp.Add(RegisteredTypes["index"].Invoke(i.Groups["ivalue"].Value));
                else if (i.Groups["function"].Success)
                    exp.Add(RegisteredTypes[i.Groups["fname"].Value].Invoke(i.Groups["fparams"].Value));

            }
            return exp;
        }

        public static Dictionary<string, Func<string, IExpressionPart>> RegisteredTypes { get; } = new Dictionary<string, Func<string, IExpressionPart>>
        {
            { "index", x => new IndexPart(int.Parse(x)) },
            { "selector", x => new SelectorPart(x) },
            { "for", x => new ForPart(x) },
            { "select", x => new SelectPart(x) },
            { "sum", x => new SumPart() },
            { "join", x => new JoinPart(x) },
            { "equals", x => new EqualsPart(x) },
            { "notEquals", x => new NotEqualsPart(x) },
            { "moreThan", x => new MoreThanPart(x) },
            { "lessThan", x => new LessThanPart(x) },
            { "equalsOrMoreThan", x => new EqualsOrMoreThanPart(x) },
            { "equalsOrLessThan", x => new EqualsOrLessThanPart(x) },
            { "iif", x => new IifPart(x) },
            { "where", x => new WherePart(x) },
            { "self", x => new SelfPart() },
            { "toLower", x => new ToLowerPart() },
            { "toUpper", x => new ToUpperPart() },
            { "groupBy", x => new GroupByPart(x) },
            { "map", x => new MapPart(x) },
            { "concat", x => new ConcatPart(x) },
            { "trim", x => new TrimPart(x) },
            { "split", x => new SplitPart(x) },
            { "avg", x => new AvgPart() },
            { "distinct", x => new DistinctPart(x) },
            { "selectMany", x => new SelectManyPart(x) }
        };
    }
}