using System.Reflection;

namespace DataAccessLanguage.Extensions
{
    public static class ReflectionExtension
    {
        public static bool TryGetPropertyInfo(this object obj, string propertyName, out PropertyInfo propertyInfo)
        {
            if ((propertyInfo = obj?.GetType()?.GetProperty(propertyName)) != null)
                return true;
            return false;
        }
        public static bool TrySetValue(this PropertyInfo property, object obj, object value)
        {
            MethodInfo method = property.GetSetMethod(true);
            if (method != null)
            {
                try
                {
                    method.Invoke(obj, new object[] { value });
                    return true;
                }
                catch { }
            }

            return false;
        }
    }
}