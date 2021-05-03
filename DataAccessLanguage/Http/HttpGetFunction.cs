using System;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace DataAccessLanguage.Http
{
    public class HttpGetFunction : IAsyncExpressionPart
    {
        public ExpressionType Type => ExpressionType.Function;

        private readonly JsonSerializerOptions jsonSerializerOptions;

        public HttpGetFunction(JsonSerializerOptions jsonSerializerOptions)
        {
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

            return await httpFunctionObject.Http.GetFromJsonAsync<object>(httpFunctionObject.Url, jsonSerializerOptions);
        }

        public Task<bool> SetValueAsync(object dataObject, object value) =>
            Task.FromResult(SetValue(dataObject, value));
    }
}