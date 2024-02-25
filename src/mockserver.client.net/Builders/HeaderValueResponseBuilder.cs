using System;
using System.Collections.Generic;
using MockServer.Client.Net.Models;

namespace MockServer.Client.Net.Builders
{
    public interface IHeaderValueResponseBuilder
    {
        IHeaderValueResponseBuilder WithValue(string value);
        IHeaderValueResponseBuilder WithValue(Func<HttpRequest, string> function);
        IEnumerable<string> Create();
    }
    
    public sealed class HeaderValueResponseBuilder : IHeaderValueResponseBuilder
    {
        private readonly HttpRequest _httpRequest;
        private readonly IList<object> _values;

        private HeaderValueResponseBuilder(HttpRequest httpRequest)
        {
            _httpRequest = httpRequest;
            _values = new List<object>();
        }

        public static IHeaderValueResponseBuilder Build(HttpRequest httpRequest)
        {
            return new HeaderValueResponseBuilder(httpRequest);
        }

        public IHeaderValueResponseBuilder WithValue(string value)
        {
            _values.Add(value);
            return this;
        }

        public IHeaderValueResponseBuilder WithValue(Func<HttpRequest, string> function)
        {
            _values.Add(function);
            return this;
        }

        public IEnumerable<string> Create()
        {
            var finalValues = new List<string>();
            foreach (var value in _values)
            {
                switch (value)
                {
                    case string s:
                        finalValues.Add(s);
                        break;
                    case Func<HttpRequest, string> function:
                        finalValues.Add(function(_httpRequest));
                        break;
                }
            }

            return finalValues;
        }
    }
}