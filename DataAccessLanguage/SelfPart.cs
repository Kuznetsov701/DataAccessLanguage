using System;

namespace DataAccessLanguage
{
    public class SelfPart : IExpressionPart
    {
        public ExpressionType Type => ExpressionType.Function;

        public object GetValue(object dataObject) => dataObject;

        public bool SetValue(object dataObject, object value) => throw new NotImplementedException("self() setValue not implemented");
    }
}