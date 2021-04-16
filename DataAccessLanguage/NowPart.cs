using System;

namespace DataAccessLanguage
{
    class NowPart : IExpressionPart
    {
        public ExpressionType Type => ExpressionType.Function;

        public object GetValue(object dataObject) => DateTime.Now;

        public bool SetValue(object dataObject, object value)
        {
            throw new NotImplementedException();
        }
    }
}