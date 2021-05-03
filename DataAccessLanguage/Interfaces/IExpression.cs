using System.Collections.Generic;

namespace DataAccessLanguage
{
    public interface IExpression : IEnumerable<IExpressionPart>, IExpressionPart 
    {
        IExpression Add(IExpressionPart expressionPart);
    }
}