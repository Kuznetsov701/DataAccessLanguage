using System;
using System.Text.RegularExpressions;

namespace DataAccessLanguage
{
    public class IifPart : IExpressionPart
    {
        private string trueValue;
        private string falseValue;

        public ExpressionType Type => ExpressionType.Function;

        public IifPart(string parameters)
        {
            Match match = new Regex(@"(?<trueValue>.*),(?<falseValue>.*)").Match(parameters);
            trueValue = match.Groups["trueValue"].Value;
            falseValue = match.Groups["falseValue"].Value;
        }

        public object GetValue(object dataObject) =>
            dataObject switch
            {
                bool b when b => trueValue,
                bool b when !b => falseValue,
                _ => null
            };

        public bool SetValue(object dataObject, object value) =>
            throw new NotImplementedException();
    }
}
