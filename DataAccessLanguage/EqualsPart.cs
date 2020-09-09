using System;

namespace DataAccessLanguage
{
    public class EqualsPart : IExpressionPart
    {
        private string parameter;
        public ExpressionType Type => ExpressionType.Function;

        public EqualsPart(string parameter)
        {
            this.parameter = parameter;
        }

        public object GetValue(object obj) =>
            obj switch
            {
                not null when parameter == "not null" => true,
                null when parameter == "null" => true,
                object o when o.ToString().Equals(parameter) => true,
                _ => false
            };

        public bool SetValue(object obj, object value) => 
            throw new NotImplementedException();
    }
}
