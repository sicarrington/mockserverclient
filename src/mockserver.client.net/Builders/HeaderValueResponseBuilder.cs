using System;
using System.Collections.Generic;
using MockServer.Client.Net.Models;

namespace MockServer.Client.Net.Builders
{
    public interface IHeaderValueResponseBuilder
    {
        IHeaderValueResponseBuilder WithValue(Func<HttpRequest, string> function);
        IHeaderValueResponseBuilder WithValue(string value);
        IHeaderValueResponseBuilder WithValue(SchemaValue schemaValue);
        IEnumerable<object> Create();
    }
    
    public sealed class HeaderValueResponseBuilder : HeaderValuesBuilderBase, IHeaderValueResponseBuilder
    {
        private readonly HttpRequest _httpRequest;

        private HeaderValueResponseBuilder(HttpRequest httpRequest) : base(new List<object>())
        {
            _httpRequest = httpRequest;
        }

        public static IHeaderValueResponseBuilder Build(HttpRequest httpRequest)
        {
            return new HeaderValueResponseBuilder(httpRequest);
        }

        public new IHeaderValueResponseBuilder WithValue(string value)
        {
            base.WithValue(value);
            return this;
        }

        public IHeaderValueResponseBuilder WithValue(Func<HttpRequest, string> function)
        {
            _values.Add(function);
            return this;
        }

        public new IHeaderValueResponseBuilder WithValue(SchemaValue schemaValue)
        {
            base.WithValue(schemaValue);
            return this;
        }

        public IEnumerable<object> Create()
        {
            var finalValues = new List<object>();
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
                    case SchemaValue schemaValue:
                        finalValues.Add(new { schema = schemaValue });
                        break;
                }
            }

            return finalValues;
        }
    }
}