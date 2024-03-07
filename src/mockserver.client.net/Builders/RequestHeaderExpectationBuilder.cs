using System;
using System.Collections.Generic;

namespace MockServer.Client.Net.Builders
{
    public interface IRequestHeaderExpectationBuilder
    {
        IRequestHeaderExpectationBuilder WithName(string name);
        IRequestHeaderExpectationBuilder WithValues(Func<IRequestHeaderValuesExpectationBuilder, IRequestHeaderValuesExpectationBuilder> requestHeaderValueBuilder = null);
        KeyValuePair<string, IEnumerable<object>> Create();
    }
    
    public sealed class RequestHeaderExpectationBuilder : IRequestHeaderExpectationBuilder
    {
        private string _name;
        private Func<IRequestHeaderValuesExpectationBuilder, IRequestHeaderValuesExpectationBuilder> _requestHeaderValueBuilder;

        private RequestHeaderExpectationBuilder()
        {
        }

        public static IRequestHeaderExpectationBuilder Build()
        {
            return new RequestHeaderExpectationBuilder();
        }

        public IRequestHeaderExpectationBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public IRequestHeaderExpectationBuilder WithValues(Func<IRequestHeaderValuesExpectationBuilder, IRequestHeaderValuesExpectationBuilder> requestHeaderValueBuilder = null)
        {
            _requestHeaderValueBuilder = requestHeaderValueBuilder;
            return this;
        }

        public KeyValuePair<string, IEnumerable<object>> Create()
        {
            if (_requestHeaderValueBuilder == null)
            {
                throw new InvalidOperationException("No Values specified for header");
            }
            if (string.IsNullOrWhiteSpace(_name))
            {
                throw new InvalidOperationException("No Name specified for header");
            }
            
            return new KeyValuePair<string, IEnumerable<object>>(_name,
                _requestHeaderValueBuilder(RequestHeaderValuesExpectationBuilder.Build()).Create());
        }
    }
}