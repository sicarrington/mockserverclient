using MockServer.Client.Net.Models;
using System;
using System.Net.Http;

namespace MockServer.Client.Net.Builders
{
    public class RequestBuilder
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
        
        public virtual RequestBuilder WithPath(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException(nameof(path));
            }
            _httpRequest.Path = path;
            return this;
        }
        public virtual RequestBuilder WithMethod(HttpMethod method)
        {
            _httpRequest.Method = method.Method;
            return this;
        }
        public virtual RequestBuilder WithBody(string requestBody)
        {
            if (requestBody == null)
            {
                throw new ArgumentNullException(nameof(requestBody));
            }
            _httpRequest.Body = requestBody;
            return this;
        }
        public virtual HttpRequest Create()
        {
            return _httpRequest;
        }
    }
}