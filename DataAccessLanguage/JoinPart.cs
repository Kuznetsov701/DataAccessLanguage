using System;
using System.Collections.Generic;

namespace DataAccessLanguage
{
    public sealed class JoinPart : IExpressionPart
    {
        private string separator;

        public ExpressionType Type => ExpressionType.Function;

        public JoinPart(string separator) => this.separator = separator;

        public object GetValue(object dataObject) =>
            dataObject switch
            {
                IEnumerable<object> list => string.Join(separator, list),
                not null => string.Join(separator, dataObject),
                _ => null
            };

        public bool SetValue(object dataObject, object value) =>
            throw new NotImplementedException();
    }
}