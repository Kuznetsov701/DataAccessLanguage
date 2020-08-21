using System.Collections.Generic;
using System.ComponentModel;

namespace DataAccessLanguage.Demo
{
    public class DataObject : INotifyPropertyChanged
    {
        private IExpressionFactory expressionFactory;

        private Dictionary<string, IExpression> cashedExpression = new Dictionary<string, IExpression>();

        public event PropertyChangedEventHandler PropertyChanged;

        public Dictionary<string, object> Object { get; private set; }

        public DataObject(Dictionary<string, object> obj, IExpressionFactory expressionFactory)
        {
            this.expressionFactory = expressionFactory;
            this.Object = obj;
        }

        public object this[string key]
        {
            get {
                if (string.IsNullOrEmpty(key))
                    return null;

                IExpression expression;
                if (cashedExpression.ContainsKey(key))
                    expression = cashedExpression[key];
                else
                    cashedExpression.Add(key, expression = expressionFactory.Create(key));
                return expression.GetValue(Object);
            }
            set {

                IExpression expression;
                if (cashedExpression.ContainsKey(key))
                    expression = cashedExpression[key];
                else
                    cashedExpression.Add(key, expression = expressionFactory.Create(key));
                if (expression.SetValue(Object, value))
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs($"[{key}]"));
            }
        }
    }
}
