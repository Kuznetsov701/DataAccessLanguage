using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccessLanguage
{
    public class SumPart : IAsyncExpressionPart
    {
        public ExpressionType Type => ExpressionType.Function;

        public object GetValue(object dataObject) =>
            dataObject switch {
                IEnumerable<int> list => list.Sum(),
                IEnumerable<long> list => list.Sum(),
                IEnumerable<float> list => list.Sum(),
                IEnumerable<double> list => list.Sum(),
                IEnumerable<decimal> list => list.Sum(),
                IEnumerable<int?> list => list.Sum(),
                IEnumerable<long?> list => list.Sum(),
                IEnumerable<float?> list => list.Sum(),
                IEnumerable<double?> list => list.Sum(),
                IEnumerable<decimal?> list => list.Sum(),
                IEnumerable<object> list => list.Select(x => { double.TryParse(x?.ToString(), out double d); return d; }).Sum(),
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