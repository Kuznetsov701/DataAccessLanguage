namespace DataAccessLanguage
{
    public interface IAsyncExpressionFactory
    {
        IAsyncExpression Create(string expression);
    }
}