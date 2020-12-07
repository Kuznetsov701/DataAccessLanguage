using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessLanguage
{
    public class ToUpperPart : IExpressionPart
    {
        public ExpressionType Type => ExpressionType.Function;

        public object GetValue(object obj) =>
            obj switch
            {
                IEnumerable<string> list => list.Select(x => x?.ToUpper()).ToList(),
                IEnumerable<object> list => list.Select(x => x?.ToString()?.ToUpper()).ToList(),
                string x => x.ToUpper(),
                object x => x.ToString().ToUpper(),
                _ => null
            };

        public bool SetValue(object dataObject, object value) =>
            throw new NotImplementedException();
    }
}
