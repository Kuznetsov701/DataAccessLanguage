using DataAccessLanguage.Http;
using DataAccessLanguage.Types;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace DataAccessLanguage.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void AddDataAccessLanguage(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IExpressionFactory>(x => new ExpressionFactory(GetDefaultTypes));
            serviceCollection.AddSingleton<IAsyncExpressionFactory>(x => new ExpressionFactory(GetDefaultTypes));
        }

        public static void AddDataAccessLanguage(this IServiceCollection serviceCollection, Action<IHttpClientBuilder> configureHttp)
        {
            var httpBuilder = serviceCollection.AddHttpClient<IExpressionFactory, ExpressionFactory>((h, s) => {
                var res = new ExpressionFactory(x => { 
                    var r = GetDefaultTypes(x);
                    r.Add("http", y => new HttpFunction(h, x, x, y));
                    r.Add("post", y => new HttpPostFunction(x, null, y));
                    r.Add("get", y => new HttpGetFunction(null));
                    return r;
                });
                return res;
            });
            configureHttp?.Invoke(httpBuilder);
            httpBuilder = serviceCollection.AddHttpClient<IAsyncExpressionFactory, ExpressionFactory>((h, s) => {
                var res = new ExpressionFactory(x => {
                    var r = GetDefaultTypes(x);
                    r.Add("http", y => new HttpFunction(h, x, x, y));
                    r.Add("post", y => new HttpPostFunction(x, null, y));
                    r.Add("get", y => new HttpGetFunction(null));
                    return r;
                });
                return res;
            });
            configureHttp?.Invoke(httpBuilder);
        }

        public static void AddDataAccessLanguage(this IServiceCollection serviceCollection, Action<IHttpClientBuilder> configureHttp, JsonSerializerOptions jsonSerializerOptions)
        {
            var httpBuilder = serviceCollection.AddHttpClient<IExpressionFactory, ExpressionFactory>((h, s) => {
                var res = new ExpressionFactory(x => {
                    var r = GetDefaultTypes(x);
                    r.Add("http", y => new HttpFunction(h, x, x, y));
                    r.Add("post", y => new HttpPostFunction(x, jsonSerializerOptions, y));
                    r.Add("get", y => new HttpGetFunction(jsonSerializerOptions));
                    return r;
                });
                return res;
            });
            configureHttp?.Invoke(httpBuilder);
            httpBuilder = serviceCollection.AddHttpClient<IAsyncExpressionFactory, ExpressionFactory>((h, s) => {
                var res = new ExpressionFactory(x => {
                    var r = GetDefaultTypes(x);
                    r.Add("http", y => new HttpFunction(h, x, x, y));
                    r.Add("post", y => new HttpPostFunction(x, jsonSerializerOptions, y));
                    r.Add("get", y => new HttpGetFunction(jsonSerializerOptions));
                    return r;
                });
                return res;
            });
            configureHttp?.Invoke(httpBuilder);
        }

        private static Dictionary<string, Func<string, IAsyncExpressionPart>> GetDefaultTypes(ExpressionFactory expressionFactory)
        {
            return new Dictionary<string, Func<string, IAsyncExpressionPart>>
             {
                { "index", x => new IndexPart(int.Parse(x)) },
                { "selector", x => new SelectorPart(x) },
                { "for", x => new ForPart(x) },
                { "select", x => new SelectPart(expressionFactory, x) },
                { "sum", x => new SumPart() },
                { "join", x => new JoinPart(x) },
                { "equals", x => new EqualsPart(x) },
                { "notEquals", x => new NotEqualsPart(x) },
                { "moreThan", x => new MoreThanPart(x) },
                { "lessThan", x => new LessThanPart(x) },
                { "equalsOrMoreThan", x => new EqualsOrMoreThanPart(x) },
                { "equalsOrLessThan", x => new EqualsOrLessThanPart(x) },
                { "iif", x => new IifPart(x) },
                { "where", x => new WherePart(expressionFactory, x) },
                { "self", x => new SelfPart() },
                { "toLower", x => new ToLowerPart() },
                { "toUpper", x => new ToUpperPart() },
                { "groupBy", x => new GroupByPart(expressionFactory, x) },
                { "map", x => new MapPart(expressionFactory, x) },
                { "switch", x => new SwitchPart(expressionFactory, x) },
                { "concat", x => new ConcatPart(expressionFactory, x) },
                { "trim", x => new TrimPart(x) },
                { "split", x => new SplitPart(x) },
                { "replace", x => new ReplacePart(x) },
                { "avg", x => new AvgPart() },
                { "distinct", x => new DistinctPart(expressionFactory, x) },
                { "selectMany", x => new SelectManyPart(expressionFactory, x) },
                { "now", x => new NowPart() },
                { "console", x => new ConsolePart() },

                { "str", x => new StringFunction(x) },
                { "int", x => new IntegerFunction(x) },
                { "double", x => new DoubleFunction(x) },
                { "float", x => new FloatFunction(x) },
                { "date", x => new DateTimeFunction(x) },
                { "time", x => new TimeSpanFunction(x) },
                { "bool", x => new BooleanFunction(x) },
                { "long", x => new LongFunction(x) },
                { "byte", x => new ByteFunction(x) },

                { "dal", x => new DalPart(expressionFactory, x) },
                { "format", x => new FormatPart(x) },

                //{ "http", x => new HttpFunction(httpClient, this, this, x) },
                //{ "post", x => new HttpPostFunction(this, x) },
                //{ "get", x => new HttpGetFunction() },
            };
        }
    }
}