using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace DataAccessLanguage.Http
{
    public class HttpFunction : IAsyncExpressionPart
    {
        private readonly string urlExpression;
        private readonly HttpClient httpClient;
        private readonly IExpressionFactory expressionFactory;
        private readonly IAsyncExpressionFactory asyncExpressionFactory;

        private IExpression expression;
        private IAsyncExpression asyncExpression;

        public ExpressionType Type => ExpressionType.Function;

        public HttpFunction(HttpClient httpClient, IExpressionFactory expressionFactory, IAsyncExpressionFactory asyncExpressionFactory, string urlExpression)
        {
            this.urlExpression = urlExpression;
            this.httpClient = httpClient;
            this.expressionFactory = expressionFactory;
            this.asyncExpressionFactory = asyncExpressionFactory;
        }

        public object GetValue(object dataObject) {
            expression ??= expressionFactory.Create(urlExpression);
            return new HttpFunctionObject { 
                DataObject = dataObject, 
                Http = httpClient, 
                Url = expression.GetValue(dataObject)?.ToString() 
            };
        }

        public bool SetValue(object dataObject, object value) =>
            throw new NotImplementedException();

        public async Task<object> GetValueAsync(object dataObject)
        {
            asyncExpression ??= asyncExpressionFactory.Create(urlExpression);
            return new HttpFunctionObject { 
                DataObject = dataObject, 
                Http = httpClient, 
                Url = (await asyncExpression.GetValueAsync(dataObject))?.ToString() 
            };
        }

        public Task<bool> SetValueAsync(object dataObject, object value) =>
            Task.FromResult(SetValue(dataObject, value));
    }
}