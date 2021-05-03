using System.Collections.Generic;

namespace DataAccessLanguage
{
    public interface IAsyncExpression : IEnumerable<IAsyncExpressionPart>, IAsyncExpressionPart
    {
        IAsyncExpression Add(IAsyncExpressionPart expressionPart);
    }
}