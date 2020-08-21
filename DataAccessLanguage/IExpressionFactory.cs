namespace DataAccessLanguage
{
    public interface IExpressionFactory
    {
        IExpression Create(string expression);
    }
}