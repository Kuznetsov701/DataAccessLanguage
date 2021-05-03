using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccessLanguage
{
    public class ToLowerPart : IAsyncExpressionPart
    {     
        public ExpressionType Type => ExpressionType.Function;

        public object GetValue(object obj) =>
            obj switch
            {
                IEnumerable<string> list => list.Select(x => x?.ToLower()).ToList(),
                IEnumerable<object> list => list.Select(x => x?.ToString()?.ToLower()).ToList(),
                string x => x.ToLower(),
                object x => x.ToString().ToLower(),
                _ => null
            };

        public bool SetValue(object dataObject, object value) =>
            throw new NotImplementedException();

        public Task<object> GetValueAsync(object dataObject) =>
            Task.FromResult(GetValue(dataObject));

        public Task<bool> SetValueAsync(object dataObject, object value) =>
            Task.FromResult(SetValue(dataObject, value));
    }
}