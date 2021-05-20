using System.Threading.Tasks;

namespace DataAccessLanguage
{
    public sealed class DalPart : IAsyncExpressionPart
    {
        private IExpression expression;
        private readonly IExpressionFactory expressionFactory;

        public ExpressionType Type => ExpressionType.Function;

        public DalPart(IExpressionFactory expressionFactory, string parameter)
        {
            this.expression = expressionFactory.Create(parameter);
            this.expressionFactory = expressionFactory;
        }

        public object GetValue(object obj)
        {
            string strExpr = expression.GetValue(obj)?.ToString();
            IExpression expr = expressionFactory.Create(strExpr);
            return expr.GetValue(obj);
        }
        public bool SetValue(object obj, object value)
        {
            string strExpr = expression.GetValue(obj)?.ToString();
            IExpression expr = expressionFactory.Create(strExpr);
            return expr.SetValue(obj, value);
        }

        public Task<object> GetValueAsync(object dataObject) =>
            Task.FromResult(GetValue(dataObject));

        public Task<bool> SetValueAsync(object dataObject, object value) =>
            Task.FromResult(SetValue(dataObject, value));
    }
}