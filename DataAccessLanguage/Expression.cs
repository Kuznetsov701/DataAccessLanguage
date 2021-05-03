using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccessLanguage
{
    public class Expression : IExpression, IAsyncExpression
    {
        private ExpressionNode head;
        private ExpressionNode tail;

        public int Count { get; private set; } = 0;

        public ExpressionType Type => ExpressionType.Function;

        public IAsyncExpression Add(IAsyncExpressionPart expressionPart)
        {
            ExpressionNode node = new ExpressionNode { Current = expressionPart };

            if (head == null)
                head = node;
            else
                tail.Next = node;
            tail = node;

            Count++;

            return this;
        }

        public IExpression Add(IExpressionPart expressionPart)
        {
            this.Add(expressionPart as IAsyncExpressionPart);
            return this;
        }

        public object GetValue(object dataObject)
        {
            object res = dataObject;
            foreach (var i in this)
                res = i.GetValue(res);
            return res;
        }

        public bool SetValue(object dataObject, object value)
        {
            object obj = dataObject;
            ExpressionNode node = head;
            for (int i = 0; i < Count - 1; i++)
            {
                object tmp = null;
                if ((tmp = node.Current.GetValue(obj)) == null)
                {
                    if (node.Next.Current.Type == ExpressionType.Index)
                        node.Current.SetValue(obj, tmp = new List<object>());
                    else if (node.Next.Current.Type == ExpressionType.Selector)
                        node.Current.SetValue(obj, tmp = new Dictionary<string, object>());
                }
                obj = tmp;
                node = node.Next;
            }

            return tail.Current.SetValue(obj, value);
        }

        public IEnumerator<IExpressionPart> GetEnumerator()
        {
            ExpressionNode node = head;
            while (node != null)
            {
                yield return node.Current;
                node = node.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator<IAsyncExpressionPart> IEnumerable<IAsyncExpressionPart>.GetEnumerator()
        {
            ExpressionNode node = head;
            while (node != null)
            {
                yield return node.Current;
                node = node.Next;
            }
        }

        public async Task<object> GetValueAsync(object dataObject)
        {
            object res = dataObject;
            foreach (IAsyncExpressionPart i in this)
                res = await i.GetValueAsync(res);
            return res;
        }

        public async Task<bool> SetValueAsync(object dataObject, object value)
        {
            object obj = dataObject;
            ExpressionNode node = head;
            for (int i = 0; i < Count - 1; i++)
            {
                object tmp = null;
                if ((tmp = await node.Current.GetValueAsync(obj)) == null)
                {
                    if (node.Next.Current.Type == ExpressionType.Index)
                        await node.Current.SetValueAsync(obj, tmp = new List<object>());
                    else if (node.Next.Current.Type == ExpressionType.Selector)
                        await node.Current.SetValueAsync(obj, tmp = new Dictionary<string, object>());
                }
                obj = tmp;
                node = node.Next;
            }

            return await tail.Current.SetValueAsync(obj, value);
        }

        private class ExpressionNode
        {
            public IAsyncExpressionPart Current { get; set; }
            public ExpressionNode Next { get; set; }
        }
    }
}
