using System;
using System.Collections.Generic;
using System.Linq;
using MockServer.Client.Net.Models;

namespace MockServer.Client.Net.Builders
{    
    public sealed class ResponseBuilder : IResponseBuilder
    {
        private readonly HttpRequest _httpRequest;
        private readonly HttpResponse _httpResponse;

        private Func<IHeadersResponseBuilder, IHeadersResponseBuilder> _headersResponseBuilder = null;
        private IDictionary<string, IEnumerable<string>> _headers;
        
        private ResponseBuilder(HttpRequest request, HttpResponse response)
        {
            _httpRequest = request;
            _httpResponse = response;
        }

        public static IResponseBuilder Build(HttpRequest httpRequest, IHeadersResponseBuilder headersResponseBuilder = null)
        {
            
            return new ResponseBuilder(httpRequest, new HttpResponse());
        }

        public HttpResponse Create()
        {
            if (_headersResponseBuilder != null)
            {
                _httpResponse.Headers = _headersResponseBuilder(HeadersResponseBuilder.Build(_httpRequest)).Create();
            }
            else if (_headers != null)
            {
                _httpResponse.Headers = _headers.ToDictionary(pair => pair.Key, 
                    pair => (IEnumerable<object>)pair.Value.Select(x => (object)x).ToList());
            }

            return _httpResponse;
        }

        public IResponseBuilder WithStatusCode(int statusCode)
        {
            _httpResponse.StatusCode = statusCode;
            return this;
        }

        public IResponseBuilder WithHeaders(Func<IHeadersResponseBuilder, IHeadersResponseBuilder> headersResponseBuilder = null)
        {
            _headers = null;
            _headersResponseBuilder = headersResponseBuilder;
            return this;
        }

        public IResponseBuilder WithHeaders(IDictionary<string, IEnumerable<string>> headers)
        {
            _headersResponseBuilder = null;
            _headers = headers;
            return this;
        }
    }
}