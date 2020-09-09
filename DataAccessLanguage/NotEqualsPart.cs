using System;

namespace DataAccessLanguage
{
    public class NotEqualsPart : IExpressionPart
    {
        private string parameter;
        public ExpressionType Type => ExpressionType.Function;

        public NotEqualsPart(string parameter)
        {
            this.parameter = parameter;
        }

        public object GetValue(object obj) =>
            obj switch
            {
                not null when parameter == "not null" => false,
                null when parameter == "null" => false,
                object o when o.ToString().Equals(parameter) => false,
                _ => true
            };

        public bool SetValue(object obj, object value) =>
            throw new NotImplementedException();
    }
}