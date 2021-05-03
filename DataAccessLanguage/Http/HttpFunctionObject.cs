using System.Net.Http;

namespace DataAccessLanguage.Http
{
    public class HttpFunctionObject
    {
        public HttpClient Http { get; set; }
        public object DataObject { get; set; }
        public string Url { get; set; }
    }
}