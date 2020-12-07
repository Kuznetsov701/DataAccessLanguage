using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessLanguage
{
    public class ToLowerPart : IExpressionPart
    {     
        public ExpressionType Type => ExpressionType.Function;

        public object GetValue(object obj) =>
            obj switch
            {
                IEnumerable<string> list => list.Select(x => x?.ToLower()).ToList(),
                IEnumerable<object> list => list.Select(x => x?.ToString()?.ToLower()).ToList(),
                string x => x.ToLower(),
                object x => x.ToString().ToLower(),
                _ => null
            };

        public bool SetValue(object dataObject, object value) =>
            throw new NotImplementedException();
    }
}
