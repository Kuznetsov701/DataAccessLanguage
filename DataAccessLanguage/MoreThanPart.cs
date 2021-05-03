﻿using System;
using System.Threading.Tasks;

namespace DataAccessLanguage
{
    public class MoreThanPart : IAsyncExpressionPart
    {
        private string parameter;
        public ExpressionType Type => ExpressionType.Function;

        public MoreThanPart(string parameter)
        {
            this.parameter = parameter;
        }

        public object GetValue(object obj) =>
            obj switch
            {
                not null when double.TryParse(obj.ToString(), out double a) && double.TryParse(parameter, out double b) => a > b,
                DateTime a when DateTime.TryParse(parameter, out DateTime b) => a > b,
                TimeSpan a when TimeSpan.TryParse(parameter, out TimeSpan b) => a > b,
                not null when DateTime.TryParse(obj.ToString(), out DateTime a) && DateTime.TryParse(parameter, out DateTime b) => a > b,
                not null when TimeSpan.TryParse(obj.ToString(), out TimeSpan a) && TimeSpan.TryParse(parameter, out TimeSpan b) => a > b,
                _ => null
            };

        public bool SetValue(object obj, object value) =>
            throw new NotImplementedException();

        public Task<object> GetValueAsync(object dataObject) =>
            Task.FromResult(GetValue(dataObject));

        public Task<bool> SetValueAsync(object dataObject, object value) =>
            Task.FromResult(SetValue(dataObject, value));
    }
}
