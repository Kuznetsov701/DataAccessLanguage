using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DataAccessLanguage
{
    public sealed class ReplacePart : IAsyncExpressionPart
    {
        private string oldValue;
        private string newValue;

        public ExpressionType Type => ExpressionType.Function;

        public ReplacePart(string parameters)
        {
            Match match = new Regex(@"(?<oldValue>.*)=>(?<newValue>.*)").Match(parameters);
            oldValue = match.Groups["oldValue"].Value;
            newValue = match.Groups["newValue"].Value;
        }

        public object GetValue(object dataObject) =>
            dataObject switch
            {
                IEnumerable<string> list => list?.Select(x => x?.Replace(oldValue, newValue))?.ToList(),
                string s => s?.Replace(oldValue, newValue),
                IEnumerable<object> list => list?.Select(x => x?.ToString()?.Replace(oldValue, newValue))?.ToList(),
                not null => dataObject?.ToString()?.Replace(oldValue, newValue),
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