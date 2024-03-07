using System;
using System.Collections.Generic;

namespace MockServer.Client.Net.Builders
{
    public sealed class RequestHeadersExpectationBuilder : IRequestHeadersExpectationBuilder
    {
        private readonly IDictionary<string, IEnumerable<object>> _headers;
        
        private RequestHeadersExpectationBuilder()
        {
            _headers = new Dictionary<string, IEnumerable<object>>();
        }

        public static IRequestHeadersExpectationBuilder Build()
        {
            return new RequestHeadersExpectationBuilder();
        }
        
        public IRequestHeadersExpectationBuilder WithHeader(Func<IRequestHeaderExpectationBuilder, IRequestHeaderExpectationBuilder> headerResponseBuilder)
        {
            if (headerResponseBuilder == null)
            {
                throw new ArgumentNullException(nameof(headerResponseBuilder));
            }
            _headers.Add(headerResponseBuilder(RequestHeaderExpectationBuilder.Build()).Create());
            return this;
        }

        public IDictionary<string, IEnumerable<object>> Create()
        {
            return _headers;
        }
    }
}