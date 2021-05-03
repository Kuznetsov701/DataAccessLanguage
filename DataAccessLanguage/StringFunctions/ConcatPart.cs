using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DataAccessLanguage
{
    public class ConcatPart : IAsyncExpressionPart
    {
        private enum ConcatItemType
        {
            Text = 1,
            Expression = 2
        }

        private record ConcatItem(ConcatItemType Type, string Text, IExpression Expression);

        private ICollection<ConcatItem> concatItems = new List<ConcatItem>();

        public ExpressionType Type => ExpressionType.Function;

        public ConcatPart(IExpressionFactory expressionFactory, string parameter)
        {
            Regex regex = new Regex(@"(""(?<text>[^&]+)"")|(?<expr>([^()&""]+|(\((?>[^()]+|\((?<depth>)|\)(?<-depth>))*(?(depth)(?!))\)))+)");

            foreach (Match x in regex.Matches(parameter))
            {
                if (x.Groups["text"].Success && !string.IsNullOrEmpty(x.Groups["text"].Value))
                    concatItems.Add(new ConcatItem(ConcatItemType.Text, x.Groups["text"].Value, null));
                else if(x.Groups["expr"].Success && !string.IsNullOrWhiteSpace(x.Groups["expr"].Value))
                    concatItems.Add(new ConcatItem(ConcatItemType.Expression, null, expressionFactory.Create(x.Groups["expr"].Value)));
            }
        }

        public object GetValue(object obj) =>
            obj switch
            {
                object o => Map(o),
                _ => null
            };

        private object Map(object o)
        {
            string res = "";
            foreach (var i in concatItems)
            {
                if (i.Type == ConcatItemType.Text)
                    res += i.Text;
                else if(i.Type == ConcatItemType.Expression)
                    res += i.Expression.GetValue(o)?.ToString();
            }
            return res;
        }

        public bool SetValue(object obj, object value) =>
            throw new NotImplementedException();

        public Task<object> GetValueAsync(object dataObject) =>
            Task.FromResult(GetValue(dataObject));

        public Task<bool> SetValueAsync(object dataObject, object value) =>
            Task.FromResult(SetValue(dataObject, value));
    }
}
