using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace DataAccessLanguage.Http
{
    public class HttpPostFunction : IAsyncExpressionPart
    {
        private readonly IExpression expression;
        private readonly JsonSerializerOptions jsonSerializerOptions;

        public ExpressionType Type => ExpressionType.Function;

        public HttpPostFunction(IExpressionFactory expressionFactory, JsonSerializerOptions jsonSerializerOptions, string expression)
        {
            this.expression = expressionFactory.Create(expression);
            this.jsonSerializerOptions = jsonSerializerOptions;
        }

        public object GetValue(object dataObject) =>
            GetValueAsync(dataObject).Result;

        public bool SetValue(object dataObject, object value) =>
            throw new NotImplementedException();

        public async Task<object> GetValueAsync(object dataObject)
        {
            HttpFunctionObject httpFunctionObject = dataObject as HttpFunctionObject;
            if (httpFunctionObject == null)
                return null;

            HttpResponseMessage response = await httpFunctionObject.Http.PostAsJsonAsync(httpFunctionObject.Url, expression.GetValue(httpFunctionObject.DataObject), jsonSerializerOptions);
            return response.Content.ReadFromJsonAsync<object>(jsonSerializerOptions);
        }

        public Task<bool> SetValueAsync(object dataObject, object value) =>
            Task.FromResult(SetValue(dataObject, value));
    }
}