using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessLanguage
{
    public class AvgPart : IExpressionPart
    {
        public ExpressionType Type => ExpressionType.Function;

        public object GetValue(object dataObject) =>
            dataObject switch {
                IEnumerable<int> list => list.Average(),
                IEnumerable<long> list => list.Average(),
                IEnumerable<float> list => list.Average(),
                IEnumerable<double> list => list.Average(),
                IEnumerable<decimal> list => list.Average(),
                IEnumerable<int?> list => list.Average(),
                IEnumerable<long?> list => list.Average(),
                IEnumerable<float?> list => list.Average(),
                IEnumerable<double?> list => list.Average(),
                IEnumerable<decimal?> list => list.Average(),
                IEnumerable<object> list => list.Select(x => { double.TryParse(x?.ToString(), out double d); return d; }).Average(),
                _ => null
            };

        public bool SetValue(object dataObject, object value)
        {
            throw new NotImplementedException();
        }
    }
}