using System.Collections.Generic;

namespace DataAccessLanguage.Extensions
{
    public static class IDictionaryExtension
    {
        public static bool TrySetValue(this IDictionary<string, object> obj, string key, object value)
        {
            if (obj == null)
                return false;

            if (obj.ContainsKey(key))
                obj[key] = value;
            else
                obj.Add(key, value);
            return true;
        }
    }
}