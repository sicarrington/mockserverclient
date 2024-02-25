using MockServer.Client.Net.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace MockServer.Client.Net.Builders
{
    public sealed class RequestBuilder : IRequestBuilder
    {
        private readonly HttpRequest _httpRequest;
        
        private RequestBuilder(HttpRequest request)
        {
            _httpRequest = request;
        }

        public static IRequestBuilder Build()
        {
            return new RequestBuilder(new HttpRequest());
        }

        public IRequestBuilder WithPath(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException(nameof(path));
            }
            _httpRequest.Path = path;
            return this;
        }
        public IRequestBuilder WithMethod(HttpMethod method)
        {
            _httpRequest.Method = method.Method;
            return this;
        }
        public IRequestBuilder WithBody(string requestBody)
        {
            if (requestBody == null)
            {
                throw new ArgumentNullException(nameof(requestBody));
            }
            _httpRequest.Body = requestBody;
            return this;
        }

        public IRequestBuilder WithHeaders(IDictionary<string, IEnumerable<string>> headers)
        {
            if (headers == null)
            {
                throw new ArgumentNullException(nameof(headers));
            }

            _httpRequest.Headers = headers;
            return this;
        }

        public IRequestBuilder WithCookies(IDictionary<string, string> cookies)
        {
            if (cookies == null)
            {
                throw new ArgumentNullException(nameof(cookies));
            }

            _httpRequest.Cookies = cookies;
            return this;
        }

        public IRequestBuilder WithQueryStringParameters(IQueryStringExpectationBuilder queryStringExpectationBuilder)
        {
            if (queryStringExpectationBuilder == null)
            {
                throw new ArgumentNullException(nameof(queryStringExpectationBuilder));
            }
            
            _httpRequest.QueryStringParameters = queryStringExpectationBuilder.Create();
            return this;
        }
        
        public HttpRequest Create()
        {
            return _httpRequest;
        }
    }
}