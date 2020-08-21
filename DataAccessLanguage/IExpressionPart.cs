namespace DataAccessLanguage
{
    public interface IExpressionPart
    {
        ExpressionType Type { get; }

        object GetValue(object dataObject);
        bool SetValue(object dataObject, object value);
    }
}