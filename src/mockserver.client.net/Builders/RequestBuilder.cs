using MockServer.Client.Net.Models;
using System;
using System.Net.Http;

namespace MockServer.Client.Net.Builders
{
    public sealed class RequestBuilder
    {
        private readonly HttpRequest _httpRequest;
        private RequestBuilder(HttpRequest request)
        {
            _httpRequest = request;
        }
        public RequestBuilder()
        {
            _httpRequest = new HttpRequest();
        }

        public RequestBuilder WithPath(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException(nameof(path));
            }
            _httpRequest.Path = path;
            return this;
        }
        public RequestBuilder WithMethod(HttpMethod method)
        {
            _httpRequest.Method = method.Method;
            return this;
        }
        public RequestBuilder WithBody(string requestBody)
        {
            if (requestBody == null)
            {
                throw new ArgumentNullException(nameof(requestBody));
            }
            _httpRequest.Body = requestBody;
            return this;
        }
        
        public HttpRequest Create()
        {
            return _httpRequest;
        }
    }
}