using DataAccessLanguage.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace DataAccessLanguage
{
    public class ForPart : IExpressionPart
    {
        private int begin, end;

        public ExpressionType Type => ExpressionType.Function;

        public ForPart(string parameters)
        {
            Match match = new Regex(@"(?<begin>\d*)..(?<end>\d*)").Match(parameters);
            begin = int.TryParse(match.Groups["begin"].Value, out int b) ? b : 0;
            end = int.TryParse(match.Groups["end"].Value, out int e) ? e : -1;
        }

        public object GetValue(object obj) =>
            obj switch
            {
                IEnumerable<object> list => list.Select(begin, end == -1 ? list.Count() - 1 : end).ToList(),
                _ => null
            };

        public bool SetValue(object obj, object value) =>
            obj switch
            {
                IList<object> list => list.SetValues(begin, end == -1 ? list.Count() - 1 : end, value),
                _ => false
            };
    }
}