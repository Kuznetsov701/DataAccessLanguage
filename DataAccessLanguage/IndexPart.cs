using DataAccessLanguage.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessLanguage
{
    public sealed class IndexPart : IExpressionPart
    {
        private int index = 0;
        public ExpressionType Type => ExpressionType.Index;

        public IndexPart(int index) => this.index = index;

        public object GetValue(object dataObject) =>
            dataObject switch
            {
                IList<object> list when list.Count > index => list[index],
                IEnumerable<object> list when list.Count() > index => list.ElementAt(index),
                _ => null
            };

        public bool SetValue(object dataObject, object value) =>
            dataObject switch
            {
                IList<object> list => list.TrySetValue(index, value),
                _ => false
            };
    }
}
